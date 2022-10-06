using ExamTaskDockerUiDb.Models;
using MySql.Data.MySqlClient;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class ResponseParser
    {
        public static List<TestModel> ParseToTestModel(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToTestModel) + " \"Start parsing response from database to models\"");
            List<TestModel> testModels = new List<TestModel>();

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
            DataBaseUtils.mySqlDb.Close();
            return testModels;
        }

        public static string ParseToString(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToProjectModel) + " \"Start parsing response from database to models\"");

            reader.Read();
            string result = reader[0].ToString();
            reader.Close();
            DataBaseUtils.mySqlDb.Close();
            return result;
        }

        public static List<ProjectModel> ParseToProjectModel(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToProjectModel) + " \"Start parsing response from database to models\"");
            List<ProjectModel> testModels = new List<ProjectModel>();

            while (reader.Read())
            {
                ProjectModel model = new ProjectModel();

                model.name = reader[0].ToString();
                model.id = reader[1].ToString();
                
                testModels.Add(model);
            }
            reader.Close();
            DataBaseUtils.mySqlDb.Close();
            return testModels;
        }

        public static List<TestModel> ParseToSessionModel(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToTestModel) + " \"Start parsing response from database to models\"");
            List<TestModel> testModels = new List<TestModel>();

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
            DataBaseUtils.mySqlDb.Close();
            return testModels;
        }
    }
}
