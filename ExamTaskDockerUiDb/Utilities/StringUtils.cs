using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Models;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class StringUtils
    {
        public static List<string> SeparateString(string text, char separator)
        {
            LoggerUtils.LogStep(nameof(SeparateString) + $" \"Start separating string - [{text}]\"");
            try
            {
                string[] separatedData = text.Split(separator);

                List<string> userInfoFields = new List<string>();

                for (int i = 0; i < separatedData.Length; i++)
                {
                    userInfoFields.Add(separatedData[i].Trim());
                }
                return userInfoFields;
            }
            catch (Exception e)
            {
                LoggerUtils.LogError(nameof(SeparateString) + $" \"Can't separate string - [{text}]\"", e);
                throw;
            }
        }

        public static string ConvertDateTime(string date)
        {
            BaseTest.Logger.Info(string.Format("Start converting {0}", date));
            DateTime dateTime = Convert.ToDateTime(date);
            return dateTime.ToString(BaseTest.testData.DateTimeFormat);
        }

        public static string CreateGetTestIdSqlRequest(TestModel model)
        {
            LoggerUtils.LogStep(nameof(CreateGetTestIdSqlRequest) + " \"Start creating get test id sql request\"");
            string request = BaseTest.sqlRequests["getTestId"];
            return request.Replace("{0}", model.name);
        }

        public static string CreateGetTestModelSqlRequest(TestModel model)
        {
            LoggerUtils.LogStep(nameof(CreateGetTestModelSqlRequest) + " \"Start creating get test model by name sql request\"");
            string request = BaseTest.sqlRequests["getTestInfo"];
            return request.Replace("{0}", model.name);
        }

        public static string CreateSendAttachmentsSqlRequest(string content, string contentType, int testId)
        {
            LoggerUtils.LogStep(nameof(CreateSendAttachmentsSqlRequest) + " \"Start creating Send attachment sql request\"");
            return BaseTest.sqlRequests["addAttachments"] + "(" + "'" + content + "'" + ", " + "'" + contentType + "'" + ", " +
                   testId + ")";
        }

        public static string CreateSendLogsSqlRequest(string content, int testId)
        {
            LoggerUtils.LogStep(nameof(CreateSendLogsSqlRequest) + " \"Start creating send logs sql request\"");
            return BaseTest.sqlRequests["addLogs"] + "(" + "'" + content + "'" + ", " + testId + ")";
        }


        public static string CreateSendTestSqlRequest(TestModel model)
        {
            LoggerUtils.LogStep(nameof(CreateSendTestSqlRequest) + " \"Start creating Send test  sql request\"");
            return BaseTest.sqlRequests["SendTest"] + "(" + "'" + model.name + "'" + ", " + model.project_id + ", " + "'" + model.method_name + "'" + ", " + 
                   model.session_id + ", " + "'" + model.env + "'" + ", " +
                   "'" + model.browser + "'" + ")"; 
        }

        public static string CreateSessionSqlRequest(SessionModel model)
        {
            LoggerUtils.LogStep(nameof(CreateSessionSqlRequest) + " \"Start creating Set session sql request\"");
            return BaseTest.sqlRequests["CreateSession"] + "(" + "'" + model.session_key + "'" + ", " + model.build_number + ")"; 
        }

        public static string CreateGetProjectIdByNameRequest(string name)
        {
            LoggerUtils.LogStep(nameof(CreateGetProjectIdByNameRequest) + " \"Start creating get project id sql request\"");
            return BaseTest.sqlRequests["getProjectIdByName"] + "'" + name + "'";
        }

        public static string CreateGetSessionIdRequest(SessionModel model)
        {
            LoggerUtils.LogStep(nameof(CreateGetSessionIdRequest) + " \"Start creating get session sql request\"");
            return BaseTest.sqlRequests["getSessionId"] + "'" + model.session_key + "'";
        }

        public static string ConvertLogsToString()
        {
            LoggerUtils.LogStep(nameof(ConvertLogsToString) + " \"Start converting file to string\"");
            string logs = FileReader.ReadFile(FileConstants.PathToLogFile);
            return logs.Replace("'", "");
        }
    }
}
