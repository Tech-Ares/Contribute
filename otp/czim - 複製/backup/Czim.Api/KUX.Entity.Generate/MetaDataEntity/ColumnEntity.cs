namespace KUX.Entity.Generate
{
    public class ColumnEntity
    {
        public string DATA_TYPE { get; set; }
        public string COLUMN_NAME { get; set; }
        public string COLUMN_COMMENT { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string COLUMN_KEY { get; set; }
    }
}