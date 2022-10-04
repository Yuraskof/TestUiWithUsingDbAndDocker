using ExamTaskDockerUiDb.Models;
using MySql.Data.MySqlClient;

namespace ExamTaskDockerUiDb.Utilities
{
    public class ResponseParser
    {
        public static List<TestModel> testModels = new List<TestModel>();
        public static void ParseToModel(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                TestModel testModel = new TestModel();

                testModel.name = reader[0].ToString();
                testModel.status_id = reader[1].ToString();
                testModel.method_name = reader[2].ToString();
                testModel.session_id = reader[3].ToString();
                testModel.start_time = reader[4].ToString();
                testModel.end_time = reader[5].ToString();
                testModel.env = reader[6].ToString();
                testModel.browser = reader[7].ToString();

                testModels.Add(testModel);
            }
            reader.Close();
            DataBase.mySqlDb.Close();
        }
    }
}
