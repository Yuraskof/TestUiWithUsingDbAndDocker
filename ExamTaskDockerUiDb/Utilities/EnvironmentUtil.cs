using Aquality.Selenium.Core.Configurations;
using System.Diagnostics;
using System.Reflection;
using Aquality.Selenium.Browsers;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class EnvironmentUtil
    {
        public static string GetHostName()
        {
            LoggerUtils.LogStep(nameof(GetHostName));
            return Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        public static int GetBuildNumber()
        {
            LoggerUtils.LogStep(nameof(GetBuildNumber));
            return Convert.ToInt32(StringUtils.SeparateString(Assembly.GetExecutingAssembly().GetName().Version.ToString(), '.')[0]);
        }

        public static string GetTestName()
        {
            LoggerUtils.LogStep(nameof(GetTestName));
            return TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
        }

        public static string GetProjectName()
        {
            LoggerUtils.LogStep(nameof(GetProjectName) + " \"Get project name\"");
            StackFrame sf = new();
            string className = sf.GetMethod().DeclaringType.ToString();
            return StringUtils.SeparateString(className, '.')[0];
        }

        public static string GetBrowserName()
        {
            LoggerUtils.LogStep(nameof(GetBrowserName) + " \"Get browser name\"");
            var settingsFile = AqualityServices.Get<ISettingsFile>();
            return settingsFile.GetValue<string>(".browserName");
        }
    }
}
