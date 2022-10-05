using ExamTaskDockerUiDb.Base;

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
    }
}
