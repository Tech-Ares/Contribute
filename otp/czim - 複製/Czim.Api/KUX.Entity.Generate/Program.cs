using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KUX.Entity.Generate
{
    internal class Program
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        private static string dbname = "panda_czim";

        /// <summary>
        /// 文件夹名称
        /// </summary>
        private static string filesName = "CzimSection";

        //private static string filesName = "Framework";//系统表

        /// <summary>
        /// 项目名称
        /// </summary>
        private static string modelName = "KUX.Models";

        private static void Main(string[] args)
        {
            Console.WriteLine("请输入数据库类型：");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1：MySql");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2：MsSql");
            Console.ResetColor();

            string sqltype = Console.ReadLine();

            switch (sqltype)
            {
                case "1":
                    DoMysqlEntity();
                    break;

                case "2":
                    DoMysqlEntity();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 创建mysql对象
        /// /// </summary>
        private static void DoMysqlEntity()
        {
            DapperDbContext dbhelper = new DapperDbContext(
                $"Server=124.71.186.27;Database={dbname};Uid=root;Pwd=Hw_mysql_1!qz;port=3306; Allow User Variables=True;default command timeout=120;SslMode = None",
                $"Server=124.71.186.27;Database={dbname};Uid=root;Pwd=Hw_mysql_1!qz;port=3306; Allow User Variables=True;default command timeout=120;SslMode = None"
                );
            string baseFile = AppDomain.CurrentDomain.BaseDirectory + $"../../../../{modelName}/{filesName}";

            if (!Directory.Exists(baseFile))
            {
                Directory.CreateDirectory(baseFile);
            }

            string StrSql = $@"select TABLE_NAME
                                     ,TABLE_SCHEMA
                        from  information_schema.`TABLES` where  TABLE_SCHEMA = '{dbname}'";

            var obj = dbhelper.Query<SchemaEntity>(StrSql).ToList();

            if (obj?.Count > 0)
            {
                DbToEntity dbte = new DbToEntity();
                dbte.dbhelper = dbhelper;
                dbte.dbname = dbname;
                dbte.spacename = $"{modelName}.{filesName}";
                dbte.addusing.Add("System.ComponentModel.DataAnnotations.Schema");
                dbte.addusing.Add($"{modelName}.BaseEntity");
                dbte.inherit = "StringKeyEntity";
                dbte.inheritLong = "LongKeyEntity";
                dbte.inheritNoId = "DefaultBaseEntity";

                // 排除的字段
                dbte.ignoreColumn = new List<string>() { "isactive", "cuser", "uuser", "cdate", "udate", "id" };

                foreach (var item in obj)
                {
                    dbte.tablename = item.TABLE_NAME;
                    //var test = item.TABLE_NAME[0..3];
                    //排除系统表，生成bsv表
                    if (item.TABLE_NAME[0..3].ToLower() == "sys")
                    {
                        continue;
                    }
                    ////排除bsv表，生成系统表
                    //if (item.TABLE_NAME[0..3].ToLower() == "bsv")
                    //{
                    //    continue;
                    //}
                    var ss = dbte.dousing();

                    WirteCs(ss, (baseFile + "/" + item.ClassName + ".cs"));
                    Console.WriteLine(item.TABLE_NAME);
                }
            }
        }

        /// <summary>
        /// 执行mssql的数据类型
        /// </summary>
        private static void DoMsSqlEntity()
        {
            string sqlcon = $"Data Source=54.222.235.106;Initial Catalog={dbname};Integrated Security=false;User ID=sa;Password=keji2099;Pooling=true;Max Pool Size=300";

            MsSqlDapperDbContext dbhelper = new MsSqlDapperDbContext(sqlcon, sqlcon);

            string baseFile = AppDomain.CurrentDomain.BaseDirectory + $"../../../../{modelName}/{filesName}";


            if (!Directory.Exists(baseFile))
            {
                Directory.CreateDirectory(baseFile);
            }

            string StrSql = $@"select name as TABLE_NAME
                                		,'{dbname}' as TABLE_SCHEMA
                                from sysobjects where xtype='U';";

            var obj = dbhelper.Query<SchemaEntity>(StrSql).ToList();

            if (obj?.Count > 0)
            {
                MsSqlDbToEntity dbte = new MsSqlDbToEntity();
                dbte.dbhelper = dbhelper;
                //dbte.dbname = dbname;
                //dbte.spacename = "Domain.Model.AutoCode";
                //dbte.addusing = "Domain.Model";
                //dbte.inherit = "EntityBase<int>";
                dbte.dbname = dbname;
                dbte.spacename = $"{modelName}.{filesName}";
                dbte.addusing.Add("System.ComponentModel.DataAnnotations.Schema");
                dbte.addusing.Add($"{modelName}.BaseEntity");
                dbte.inherit = "StringKeyEntity";
                dbte.inheritLong = "LongKeyEntity";
                dbte.inheritNoId = "DefaultBaseEntity";

                foreach (var item in obj)
                {
                    dbte.tablename = item.TABLE_NAME;
                    var ss = dbte.dousing();

                    WirteCs(ss, (baseFile + "/" + item.TABLE_NAME + ".cs"));
                    Console.WriteLine(item.TABLE_NAME);
                }
            }
        }

        /// <summary>
        /// 保存到本地
        /// </summary>
        /// <param name="strCode">生成的代码</param>
        /// <param name="filePath">本地路径</param>
        public static void WirteCs(string strCode, string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fileStream.SetLength(0);
                var byts = Encoding.UTF8.GetBytes(strCode);
                fileStream.Write(byts, 0, byts.Length);
                fileStream.Close();
            }
        }
    }
}