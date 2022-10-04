using ExamTaskDockerUiDb.Base;
using MySql.Data.MySqlClient;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class DataBaseUtils
    {
        public static MySqlConnection mySqlDb;
        static string connect;

        private static void ConnectToDataBase()
        {
            LoggerUtils.LogStep(nameof(ConnectToDataBase) + " \"Connect to data base\"");
            connect = "Database=" + BaseTest.testData.DatabaseName + ";Datasource=" + BaseTest.testData.DatabaseHost + ";User=" + BaseTest.testData.DatabaseUser + ";Password=" + BaseTest.testData.DatabasePassword;
            mySqlDb = new MySqlConnection(connect);
            mySqlDb.Open();
        }
        
        public static MySqlDataReader SendRequest(string request)
        {
            ConnectToDataBase();
            LoggerUtils.LogStep(nameof(SendRequest) + $" \"Send request - [{request}]\"");
            MySqlCommand command = new MySqlCommand(request, mySqlDb);
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}
