using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUX.Entity.Generate
{
    public class DbToEntity
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public DapperDbContext dbhelper { get; set; }

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
        /// 需要增加的Using引用
        /// </summary>
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
            sb.Append("\r");
            sb.Append("using System.ComponentModel.DataAnnotations;");
            sb.Append("\r\n");

            //if (!string.IsNullOrWhiteSpace(addusing))
            //{
            //    sb.Append($"using {addusing};");
            //    sb.Append("\r\n");
            //}

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

            if ((tablename.Length <= 7 ||
                tablename.Substring(tablename.Length - 7).ToLower() != "_expand") &&
                useActiveById)
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
            string StrSql = @"select TABLE_NAME
                                	 ,TABLE_COMMENT
                                from information_schema.`TABLES`
                                where table_name=@tablename
                                and TABLE_SCHEMA=@dbname;";
            var obj = dbhelper.Query<TableEntity>(StrSql, new { tablename = tablename, dbname = dbname }).ToList().FirstOrDefault();

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

                if (!string.IsNullOrWhiteSpace(inheritNoId))
                {
                    //if ((obj.TABLE_NAME.Length > 7 &&
                    //    obj.TABLE_NAME.Substring(obj.TABLE_NAME.Length - 7).ToLower() == "_expand"))
                    //{
                    //    sb.Append($"    public class {obj.TABLE_NAME}");
                    //}
                    //else
                    //{
                    sb.Append($"    public class {obj.ClassName} : {inheritNoId}");
                    //}
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
            useActiveById = false;
            string StrSql = @"select a.DATA_TYPE
                                	,a.COLUMN_NAME
                                	,a.COLUMN_COMMENT
                                    ,a.COLUMN_KEY
                                from information_schema.`columns` a
                                JOIN information_schema.`TABLES` b on a.table_name=b.table_name and a.TABLE_SCHEMA=b.TABLE_SCHEMA
                                where b.TABLE_SCHEMA=@dbname
                                and b.table_name=@tablename;";

            var obj = dbhelper.Query<ColumnEntity>(StrSql, new { tablename = tablename, dbname = dbname }).ToList();

            var InsertSb = new StringBuilder();
            var ValueSb = new StringBuilder();

            if (obj?.Count > 0)
            {
                InsertSb.Append($"INSERT INTO `{tablename}` (");

                ValueSb.Append("\r\n");
                ValueSb.Append($"                                        VALUES (");
                foreach (var item in obj)
                {
                    // 自增id 不需要进行insert操作,不需要继承带id的基类
                    if (item.COLUMN_NAME.Trim().ToLower() == "id" &&
                        item.DATA_TYPE.Trim().ToLower() == "bigint")
                    {
                        //InsertSb.Append("\r\n");
                        //InsertSb.Append($"                                                    `{item.COLUMN_NAME}`,");

                        //ValueSb.Append("\r\n");
                        //ValueSb.Append($"                                                    @{item.COLUMN_NAME},");

                        sb = sb.Replace($": {inheritNoId}", $": {inheritLong}");//   inherit, inheritLong);
                        useActiveById = true;
                    }
                    else if (item.COLUMN_NAME.Trim().ToLower() == "id")
                    {
                        sb = sb.Replace($": {inheritNoId}", $": {inherit}");
                        useActiveById = true;
                    }

                    if (item.COLUMN_NAME.Trim().ToLower() == "id" &&
                        item.DATA_TYPE.Trim().ToLower() == "bigint")
                    {
                        // 如果是整形自增id，则新增接口不需要创建
                    }
                    else
                    {
                        InsertSb.Append("\r\n");
                        InsertSb.Append($"                                                    `{item.COLUMN_NAME}`,");

                        ValueSb.Append("\r\n");
                        ValueSb.Append($"                                                    @{item.COLUMN_NAME},");
                    }

                    //if (item.COLUMN_NAME.Trim().ToLower() == "isactive" ||
                    //    item.COLUMN_NAME.Trim().ToLower() == "cuser" ||
                    //    item.COLUMN_NAME.Trim().ToLower() == "uuser" ||
                    //    item.COLUMN_NAME.Trim().ToLower() == "cdate" ||
                    //    item.COLUMN_NAME.Trim().ToLower() == "udate" ||
                    //    item.COLUMN_NAME.Trim().ToLower() == "id")
                    //{
                    //    continue;
                    //}

                    //if (item.COLUMN_NAME.Trim().ToLower() == "id")
                    //{
                    //    if (item.DATA_TYPE.ToLower() == "bigint")
                    //    {
                    //        sb = sb.Replace(inherit, inheritLong);
                    //    }
                    //}

                    if (this.ignoreColumn?.Count > 0 &&
                        this.ignoreColumn.Contains(item.COLUMN_NAME.Trim().ToLower()))
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
                        case "char":
                            datatype = "string";
                            break;

                        case "text":
                            datatype = "string";
                            break;

                        case "datetime":
                        case "date":
                        case "timestamp":
                            datatype = "DateTime";
                            break;

                        case "int":
                        case "smallint":
                            datatype = "int";
                            break;

                        case "bigint":
                            datatype = "long";
                            break;

                        case "bit":
                        case "tinyint":
                            datatype = "bool";
                            break;

                        case "float":
                        case "double":
                            datatype = "double";
                            break;

                        case "decimal":
                            datatype = "decimal";
                            break;
                    }
                    sb.Append("\r\n");

                    if (item.COLUMN_KEY.Trim().ToUpper() == "PRI")
                    {
                        sb.Append("        [Key]");
                        sb.Append("\r\n");
                    }

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

            sb.Append($"        public string ActiveSql() => @\" update {tablename} ");
            sb.Append("\r\n");
            sb.Append("                                        set IsActive = @IsActive");
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
            sb.Append($"                                        from {tablename} ");
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