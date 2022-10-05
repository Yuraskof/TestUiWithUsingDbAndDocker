using Aquality.Selenium.Browsers;
using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Forms.Pages;
using ExamTaskDockerUiDb.Models;
using ExamTaskDockerUiDb.Models.RequestModels;
using ExamTaskDockerUiDb.Utilities;
using OpenQA.Selenium;

namespace ExamTaskDockerUiDb
{
    public class Test:BaseTest
    {
        [Test(Description = "TC-0001 Checking the website functionality using UI and Database")]
        public void TestWebUiAndDatabase()
        {
            GetAccessTokenModel model = ModelUtils.CreateGetAccessTokenModel();
            string accessToken = ApiApplicationRequest.GetAccessToken(model);
            Assert.NotNull(accessToken, "Acess token should be exist");
            Logger.Info("Step 1 completed.");

            AqualityServices.Browser.GoTo(BrowserUtils.CreateUrlWithCredentials()); 
            AqualityServices.Browser.Maximize();
            AllProjectsPage allProjectsPage = new AllProjectsPage();
            Assert.IsTrue(allProjectsPage.State.WaitForDisplayed(), $"{allProjectsPage.Name} should be presented");
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(new Cookie("token", accessToken));
            AqualityServices.Browser.Refresh();
            string footerText = allProjectsPage.GetFooterText();
            Assert.IsTrue(StringUtils.SeparateString(footerText, ':')[1] == testData.Variant, "Values should be equal");
            Logger.Info("Step 2 completed.");

            allProjectsPage.GoToProjectPage(testData.ProjectName);
            ProjectPage projectPage = new ProjectPage();
            Assert.IsTrue(projectPage.State.WaitForDisplayed(), $"{projectPage.Name} should be presented");
            List<TestModel> testModelsFromPage = projectPage.GetTestsNames();
            List<TestModel> testModelsFromDb = ResponseParser.ParseToModel(DataBaseUtils.SendRequest(sqlRequests["projectTests"]));

            Assert.Multiple(() =>
            {
                Assert.IsTrue(ModelUtils.CheckModelsDates(testModelsFromPage), "Dates should be descending");
                Assert.IsTrue(ModelUtils.CompareModels(testModelsFromPage, testModelsFromDb), "Values should be equal");
            });
            Logger.Info("Step 3 completed.");
        }
    }
}