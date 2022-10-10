using Aquality.Selenium.Browsers;
using ExamTaskDockerUiDb.Utilities;

namespace ExamTaskDockerUiDb.Base
{
    public abstract class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            FileUtils.ClearLogFile();
            LoggerUtils.Logger.Info($"Start scenario [{EnvironmentUtil.GetTestName()}]");
            DataBaseUtils.ConnectToDataBase();
        }

        [TearDown]
        public virtual void AfterEach()
        {
            AqualityServices.Browser.Quit();
            DataBaseUtils.DisconnectFromDatabase();
            LoggerUtils.LogScenarioResult();
        }
    }
}