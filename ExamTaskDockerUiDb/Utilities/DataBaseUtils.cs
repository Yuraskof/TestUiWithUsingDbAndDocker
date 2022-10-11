using MySql.Data.MySqlClient;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class DataBaseUtils
    {
        public static MySqlConnection MySqlDb;
        private static MySqlDataReader reader;
        static string connect;

        public static void ConnectToDataBase()
        {
            LoggerUtils.LogStep(nameof(ConnectToDataBase) + " 'Connect to database'");
            connect = "Database=" + FileUtils.TestData.DatabaseName + ";Datasource=" + FileUtils.TestData.DatabaseHost + ";User=" + FileUtils.TestData.DatabaseUser + ";Password=" + FileUtils.TestData.DatabasePassword;
            MySqlDb = new(connect);
            MySqlDb.Open();
        }
        
        public static MySqlDataReader SendRequest(string request)
        {
            if (reader != null)
            {
                reader.Close();
            }
            LoggerUtils.LogStep(nameof(SendRequest) + $" 'Send request - [{request}]'");
            MySqlCommand command = new(request, MySqlDb);
            reader = command.ExecuteReader();
            return reader;
        }

        public static void DisconnectFromDatabase()
        {
            MySqlDb.Close();
        }
    }
}
