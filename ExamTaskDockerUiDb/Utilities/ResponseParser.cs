using ExamTaskDockerUiDb.Models;
using MySql.Data.MySqlClient;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class ResponseParser
    {
        public static List<TestModel> ParseToTestModel(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToTestModel) + " \"Start parsing response from database to test models\"");
            List<TestModel> testModels = new List<TestModel>();

            while (reader.Read())
            {
                TestModel testModel = new TestModel();
                testModel.id = Convert.ToInt32(reader[0].ToString());
                testModel.project_id = Convert.ToInt32(reader[1].ToString());
                testModel.name = reader[2].ToString();
                testModel.status_id = reader[3].ToString();
                testModel.method_name = reader[4].ToString();
                testModel.session_id = Convert.ToInt32(reader[5].ToString());
                testModel.start_time = StringUtils.ConvertDateTime(reader[6].ToString());
                testModel.end_time = reader[7].ToString();
                testModel.env = reader[8].ToString();
                testModel.browser = reader[9].ToString();

                testModels.Add(testModel);
            }
            reader.Close();
            DataBaseUtils.mySqlDb.Close();
            return testModels;
        }

        public static string ParseToString(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToProjectModel) + " \"Start parsing response from database to string\"");
            reader.Read();
            string result = reader[0].ToString();
            reader.Close();
            DataBaseUtils.mySqlDb.Close();
            return result;
        }

        public static List<ProjectModel> ParseToProjectModel(MySqlDataReader reader)
        {
            LoggerUtils.LogStep(nameof(ParseToProjectModel) + " \"Start parsing response from database to project models\"");
            List<ProjectModel> testModels = new List<ProjectModel>();

            while (reader.Read())
            {
                ProjectModel model = new ProjectModel();

                model.name = reader[0].ToString();
                model.id = Convert.ToInt32(reader[1].ToString());
                
                testModels.Add(model);
            }
            reader.Close();
            DataBaseUtils.mySqlDb.Close();
            return testModels;
        }
    }
}
