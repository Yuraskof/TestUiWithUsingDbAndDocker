using ExamTaskDockerUiDb.Base;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class BrowserUtils
    {
        public static string CreateUrlWithCredentials()
        {
            LoggerUtils.LogStep(nameof(CreateUrlWithCredentials) + " \"Create url with credentials\"");
            return "http://" + BaseTest.loginUser.Login  + ":" + BaseTest.loginUser.Password + "@" + BaseTest.testData.Url;
        }
    }
}
