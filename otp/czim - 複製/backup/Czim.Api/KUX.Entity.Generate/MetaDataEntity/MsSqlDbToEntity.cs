using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUX.Entity.Generate
{
    /// <summary>
    /// mssql数据库转实体模型
    /// </summary>
    public class MsSqlDbToEntity
    {
        /// <summary>
        /// 数据操作
        /// </summary>
        public MsSqlDapperDbContext dbhelper { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string dbname { get; set; }

        /// <summary>
        /// 命名空间名
        /// </summary>
        public string spacename { get; set; }

        /// <summary>
        /// 需要增加的字符串
        /// </summary>
        //public string addusing { get; set; }

        public List<string> addusing { get; set; } = new List<string>();

        /// <summary>
        /// 继承字段
        /// </summary>
        public string inherit { get; set; }

        /// <summary>
        /// 长整形继承类名称
        /// </summary>
        public string inheritLong { get; set; }

        /// <summary>
        /// 继承字段没有默认id
        /// </summary>
        public string inheritNoId { get; set; }

        /// <summary>
        /// 排除字段
        /// </summary>
        public List<string> ignoreColumn { get; set; }

        /// <summary>
        /// 字符串
        /// </summary>
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 是否需要通过id进行激活和查询
        /// </summary>
        private bool useActiveById = false;

        /// <summary>
        /// 组合using语句
        /// </summary>
        /// <returns></returns>
        public string dousing()
        {
            sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append("\r");
            sb.Append("using System.Collections.Generic;");
            sb.Append("\r");
            sb.Append("using System.Text;");
            sb.Append("\r\n");

            if (addusing?.Count > 0)
            {
                addusing.ForEach(us =>
                {
                    sb.Append($"using {us};");
                    sb.Append("\r\n");
                });
            }

            sb.Append($"namespace {spacename}");
            sb.Append("\r\n");
            sb.Append("{");
            sb.Append("\r\n");
            doclass();
            dotype();

            if (tablename.Length <= 7 ||
                tablename.Substring(tablename.Length - 7).ToLower() != "_expand")
            {
                doActive();
                doGetByid();
            }
            //结尾
            doEnding();
            return sb.ToString();
        }

        /// <summary>
        /// 查询表名，生成类对象
        /// </summary>
        private void doclass()
        {
            string StrSql = $@"select a.name as TABLE_NAME
                                	,cast(isnull(b.[value], ' ')as nvarchar) as TABLE_COMMENT
                                from sys.tables a
                                left join sys.extended_properties b on a.object_id = b.major_id and b.minor_id = 0
                                where a.name = @tablename;";
            var obj = dbhelper.Query<TableEntity>(StrSql, new { tablename = tablename }).ToList().FirstOrDefault();

            if (obj != null)
            {
                sb.Append($@"    /// <summary>");
                sb.Append("\r\n");
                sb.Append($"    /// {obj.TABLE_COMMENT.Trim().Replace("\r", "").Replace("\n", "")}");
                sb.Append("\r\n");
                sb.Append("    /// </summary>");
                sb.Append("\r\n");
                sb.Append("    [Table(\"" + obj.TABLE_NAME + "\")]");
                sb.Append("\r\n");

                //if (!string.IsNullOrWhiteSpace(inherit))
                //{
                //    if ((obj.TABLE_NAME.Length > 7 &&
                //        obj.TABLE_NAME.Substring(obj.TABLE_NAME.Length - 7).ToLower() == "_expand"))
                //    {
                //        sb.Append($"    public class {obj.TABLE_NAME}");
                //    }
                //    else
                //    {
                //        sb.Append($"    public class {obj.TABLE_NAME} : {inherit}");
                //    }
                //}
                //else
                //{
                //    sb.Append($"    public class {obj.TABLE_NAME}");
                //}
                if (!string.IsNullOrWhiteSpace(inheritNoId))
                {
                    sb.Append($"    public class {obj.ClassName} : {inheritNoId}");
                }
                else
                {
                    sb.Append($"    public class {obj.ClassName}");
                }
                sb.Append("\r\n");
                sb.Append(@"    {");
            }
        }

        /// <summary>
        /// 生成字段
        /// </summary>
        private void dotype()
        {
            string StrSql = $@"select a.name as COLUMN_NAME
	                            	,b.name as DATA_TYPE
	                            	,cast(isnull(g.[value], ' ')as nvarchar) as COLUMN_COMMENT
	                            from syscolumns a
	                            left join systypes b on a.xusertype=b.xusertype
                                left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id
	                            where a.id=(select max(id) from sysobjects where xtype='u' and name=@tablename)
                                and (g.class_desc='OBJECT_OR_COLUMN' or g.class_desc is null);";

            var obj = dbhelper.Query<ColumnEntity>(StrSql, new { tablename = tablename }).ToList();

            var InsertSb = new StringBuilder();
            var ValueSb = new StringBuilder();

            if (obj?.Count > 0)
            {
                InsertSb.Append($"INSERT INTO [dbo].[{tablename}] (");

                ValueSb.Append("\r\n");
                ValueSb.Append($"                                        VALUES (");
                foreach (var item in obj)
                {
                    if (item.COLUMN_NAME.Trim().ToLower() != "id")
                    {
                        InsertSb.Append("\r\n");
                        InsertSb.Append($"                                                    [{item.COLUMN_NAME}],");

                        ValueSb.Append("\r\n");
                        ValueSb.Append($"                                                    @{item.COLUMN_NAME},");
                    }
                    if (item.COLUMN_NAME.Trim().ToLower() == "active" ||
                        item.COLUMN_NAME.Trim().ToLower() == "cid" ||
                        item.COLUMN_NAME.Trim().ToLower() == "uid" ||
                        item.COLUMN_NAME.Trim().ToLower() == "cdate" ||
                        item.COLUMN_NAME.Trim().ToLower() == "udate" ||
                        item.COLUMN_NAME.Trim().ToLower() == "id")
                    {
                        continue;
                    }

                    sb.Append("\r\n");
                    sb.Append("        /// <summary>");
                    sb.Append("\r\n");
                    sb.Append($"        /// {item.COLUMN_COMMENT.Trim().Replace("\r", "").Replace("\n", "")}");
                    sb.Append("\r\n");
                    sb.Append("        /// </summary>");

                    string datatype = "string";
                    switch (item.DATA_TYPE.ToLower())
                    {
                        case "varchar":
                        case "nvarchar":
                            datatype = "string";
                            break;

                        case "text":
                            datatype = "string";
                            break;

                        case "datetime":
                        case "datetime2":
                        case "date":
                        case "time":
                            datatype = "DateTime?";
                            break;

                        case "bit":
                        case "int":
                        case "smallint":
                            datatype = "int";
                            break;

                        case "tinyint":
                            datatype = "bool";
                            break;
                    }
                    sb.Append("\r\n");

                    sb.Append($@"        public {datatype} {item.COLUMN_NAME}");
                    sb.Append(" { get; set; }");
                }

                if (InsertSb.ToString().Substring(InsertSb.ToString().Length - 1, 1) == ",")
                {
                    InsertSb.Remove(InsertSb.Length - 1, 1);
                }
                InsertSb.Append(")");

                if (ValueSb.ToString().Substring(ValueSb.ToString().Length - 1, 1) == ",")
                {
                    ValueSb.Remove(ValueSb.Length - 1, 1);
                }
                ValueSb.Append(");");
            }
            sb.Append("\r\n");
            sb.Append("        /// <summary>");
            sb.Append("\r\n");
            sb.Append($"        /// 新增语句");
            sb.Append("\r\n");
            sb.Append("        /// </summary>");
            sb.Append("\r\n");

            sb.Append($"        public string InserSql() => @\" {InsertSb.ToString()}{ValueSb.ToString()}\";");
            sb.Append("\r\n");
        }

        /// <summary>
        /// 激活方法
        /// </summary>
        private void doActive()
        {
            sb.Append("\r\n");
            sb.Append("        /// <summary>");
            sb.Append("\r\n");
            sb.Append($"        /// 激活语句");
            sb.Append("\r\n");
            sb.Append("        /// </summary>");
            sb.Append("\r\n");

            sb.Append($"        public string ActiveSql() => @\" update [dbo].[{tablename}] ");
            sb.Append("\r\n");
            sb.Append("                                        set Active = @Active");
            sb.Append("\r\n");
            sb.Append("                                        where id = @id;\";");
            sb.Append("\r\n");
        }

        /// <summary>
        /// 通过id获取对象
        /// </summary>
        private void doGetByid()
        {
            sb.Append("\r\n");
            sb.Append("        /// <summary>");
            sb.Append("\r\n");
            sb.Append($"        /// 通过id查询对象");
            sb.Append("\r\n");
            sb.Append("        /// </summary>");
            sb.Append("\r\n");

            sb.Append("        public string GetByidSql() => @\" select * ");
            sb.Append("\r\n");
            sb.Append($"                                        from [dbo].[{tablename}] ");
            sb.Append("\r\n");
            sb.Append("                                        where id = @id;\";");
            sb.Append("\r\n");
        }

        /// <summary>
        /// 执行结尾输出
        /// </summary>
        public void doEnding()
        {
            sb.Append("    }");
            sb.Append("\r\n");
            sb.Append("}");
        }
    }
}