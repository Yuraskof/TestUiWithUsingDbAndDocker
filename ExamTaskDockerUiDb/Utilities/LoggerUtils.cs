using System.Runtime.CompilerServices;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using NUnit.Framework.Interfaces;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class LoggerUtils
    {
        public static Logger Logger => AqualityServices.Get<Logger>();
        private static TestContext.ResultAdapter Result => TestContext.CurrentContext.Result;
        private static void LogStep(string stepInfo, string stepType)
        {
            var shift = new string('#', 10);
            Logger.Info($"{shift} {stepType} {shift} {Environment.NewLine} {stepInfo}");
        }

        public static void LogError(string description, Exception exception)
        {
            Logger.Fatal($"Fatal: {description}", exception);
        }

        public static void LogStep([CallerMemberName] string stepInfo = "")
        {
            LogStep(stepInfo, stepType: "Action");
        }

        public static void LogScenarioResult()
        {
            Logger.Info($"Scenario [{EnvironmentUtil.GetTestName()}] result is {Result.Outcome.Status}!");
            if (Result.Outcome.Status != TestStatus.Passed)
            {
                Logger.Error(Result.Message);
            }
            Logger.Info(new string('=', 100));
        }
    }
}
