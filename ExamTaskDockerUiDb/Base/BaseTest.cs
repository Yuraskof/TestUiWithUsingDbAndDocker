using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Models;
using ExamTaskDockerUiDb.Utilities;
using Humanizer;
using NUnit.Framework.Interfaces;

namespace ExamTaskDockerUiDb.Base
{
    public abstract class BaseTest
    {
        protected string ScenarioName
            => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
            ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty).Humanize();

        public static Logger Logger => AqualityServices.Get<Logger>();
        private TestContext.ResultAdapter Result => TestContext.CurrentContext.Result;
        public static readonly TestData testData = JsonUtils.ReadJsonDataFromPath<TestData>(FileConstants.PathToTestData);
        public static readonly LoginUser loginUser = JsonUtils.ReadJsonDataFromPath<LoginUser>(FileConstants.PathToLoginUser);
        public static Dictionary<string, string> sqlRequests = FileReader.GetDataFromJson(FileConstants.PathToSqlRequests);
        public static readonly string sessionID = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

        [SetUp]
        public void Setup()
        {
            FileReader.ClearLogFile();
            Logger.Info($"Start scenario [{ScenarioName}]");
        }

        [TearDown]
        public virtual void AfterEach()
        {
            AqualityServices.Browser.Quit();
            LogScenarioResult();
        }

        private void LogScenarioResult()
        {
            Logger.Info($"Scenario [{ScenarioName}] result is {Result.Outcome.Status}!");
            if (Result.Outcome.Status != TestStatus.Passed)
            {
                Logger.Error(Result.Message);
            }
            Logger.Info(new string('=', 100));
        }
    }
}