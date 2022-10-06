using ExamTaskDockerUiDb.Base;
using System.Reflection;

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

        public static string CreateGetTestIdSqlRequest()
        {
            string testName = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
            string request = BaseTest.sqlRequests["getTestId"];
            return request.Replace("{0}", testName);
        }

        public static string CreateSendAttachmentsSqlRequest(string content, string contentType, int testId)
        {
            return BaseTest.sqlRequests["addAttachments"] + "(" + "'" + content + "'" + ", " + "'" + contentType + "'" + ", " +
                   testId + ")";
        }

        public static string CreateSendLogsSqlRequest(string content, int testId)
        {
            return BaseTest.sqlRequests["addLogs"] + "(" + "'" + content + "'" + ", " + testId + ")";
        }


        public static string CreateSendTestSqlRequest(string methodName, int projectId, int sessionId)
        {
            string testName = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();

            return BaseTest.sqlRequests["SendTest"] + "(" + "'" + testName + "'" + ", " + projectId + ", " + "'" + methodName + "'" + ", " + 
                                     sessionId + ", " + "'" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "'" + ", " +
                                     "'" + FileReader.GetBrowserName() + "'" + ")"; 
        }

        public static string CreateSessionSqlRequest()
        {
            int buildNumber = Convert.ToInt32(SeparateString(Assembly.GetExecutingAssembly().GetName().Version.ToString(), '.')[0]);

            return BaseTest.sqlRequests["CreateSession"] + "(" + "'" + BaseTest.sessionID + "'" + ", " + buildNumber + ")"; 
        }

        public static string CreateGetProjectIdByNameRequest(string name)
        {
            return BaseTest.sqlRequests["getProjectIdByName"] + "'" + name + "'";
        }

        public static string CreateGetSessionIdRequest(string key)
        {
            return BaseTest.sqlRequests["getSessionId"] + "'" + key + "'";
        }
    }
}
