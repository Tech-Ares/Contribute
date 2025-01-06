namespace KUX.Entity.Generate
{
    public class SchemaEntity
    {
        public string TABLE_NAME { get; set; }

        public string ClassName
        {
            get
            {
                var newName = this.TABLE_NAME;
                var first = newName[0..1];
                newName = first.ToUpper() + newName[1..];

                while (newName.Contains("_"))
                {
                    int _index = newName.IndexOf("_");
                    var _str = newName[_index..(_index + 2)];
                    newName = newName.Replace(_str, _str.Replace("_", "").ToUpper());
                }

                return newName;
            }
        }

        public string TABLE_SCHEMA { get; set; }
    }
}