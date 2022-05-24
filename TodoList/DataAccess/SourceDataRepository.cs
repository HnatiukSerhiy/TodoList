namespace TodoList.DataAccess
{
    public static class SourceDataRepository
    {
        private static string sourceName = "sql";

        private static List<string> sourceDataList = new List<string>() { "sql", "xml" };

        public static string SourceName
        {
            get 
            { 
                return sourceName; 
            }

            set 
            { 
                sourceName = value; 
            }
        }

        public static List<string> SourceDataList
        {
            get
            {
                return sourceDataList;
            }
        }

        public static void AddDataSource(string dataSource)
        {
            sourceDataList.Add(dataSource);
        }
    }
}
