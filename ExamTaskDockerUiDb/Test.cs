using Aquality.Selenium.Browsers;
using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Forms.Pages;
using ExamTaskDockerUiDb.Models;
using ExamTaskDockerUiDb.Utilities;
using OpenQA.Selenium;
using System.Diagnostics;

namespace ExamTaskDockerUiDb
{
    public class Test:BaseTest
    {
        [Test(Description = "ET-0001 Checking the website functionality using UI and Database")]
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
            List<TestModel> testModelsFromDb = ResponseParser.ParseToTestModel(DataBaseUtils.SendRequest(sqlRequests["projectId1Tests"]));

            Assert.Multiple(() =>
            {
                Assert.IsTrue(ModelUtils.CheckModelsDates(testModelsFromPage), "Dates should be descending");
                Assert.IsTrue(ModelUtils.CompareModels(testModelsFromPage, testModelsFromDb), "Values should be equal");
            });
            Logger.Info("Step 3 completed.");

            AqualityServices.Browser.GoBack();
            Assert.IsTrue(allProjectsPage.State.WaitForDisplayed(), $"{allProjectsPage.Name} should be presented");
            allProjectsPage.OpenAddProjectForm();
            ProjectModel projectModel = new ProjectModel();
            projectModel.name = FileReader.GetProjectName();
            Assert.IsTrue(allProjectsPage.addProjectForm.State.WaitForDisplayed(), $"{allProjectsPage.addProjectForm.Name} should be presented");
            allProjectsPage.addProjectForm.AddProject(projectModel.name);
            allProjectsPage.CloseAddProjectForm();
            Assert.IsTrue(allProjectsPage.addProjectForm.State.WaitForNotDisplayed(), $"{allProjectsPage.addProjectForm.Name} shouldn't be presented");
            AqualityServices.Browser.Refresh();
            Assert.IsTrue(allProjectsPage.ProjectIsPresented(FileReader.GetProjectName()), "Project should be presented");
            Logger.Info("Step 4 completed.");

            allProjectsPage.GoToProjectPage(FileReader.GetProjectName());
            projectModel.id = Convert.ToInt32(ResponseParser.ParseToString(DataBaseUtils.SendRequest(StringUtils.CreateGetProjectIdByNameRequest(FileReader.GetProjectName()))));
            
            SessionModel sessionModel = new SessionModel();
            sessionModel.build_number = FileReader.GetBuildNumber();
            sessionModel.session_key = sessionID;
            DataBaseUtils.SendRequest(StringUtils.CreateSessionSqlRequest(sessionModel));
            sessionModel.id = Convert.ToInt32(ResponseParser.ParseToString(DataBaseUtils.SendRequest(StringUtils.CreateGetSessionIdRequest(sessionModel))));

            TestModel testModel = new TestModel();
            StackFrame sf = new StackFrame();
            testModel.session_id = sessionModel.id;
            testModel.project_id = projectModel.id;
            testModel.name = FileReader.GetTestName();
            testModel.env = FileReader.GetHostName();
            testModel.browser = FileReader.GetBrowserName();
            testModel.method_name = StringUtils.SeparateString(sf.GetMethod().ToString(), ' ')[1];
            DataBaseUtils.SendRequest(StringUtils.CreateSendTestSqlRequest(testModel));
            testModel = ResponseParser.ParseToTestModel(DataBaseUtils.SendRequest(StringUtils.CreateGetTestModelSqlRequest(testModel)))[0];

            AqualityServices.Browser.Driver.GetScreenshot().SaveAsFile(FileConstants.PathToScreenshot + "screenshot.png");
            string screenshotString = AqualityServices.Browser.Driver.GetScreenshot().AsBase64EncodedString;
            DataBaseUtils.SendRequest(StringUtils.CreateSendAttachmentsSqlRequest(screenshotString, testData.ImageContentType, testModel.id));
            string logs = StringUtils.ConvertLogsToString();
            DataBaseUtils.SendRequest(StringUtils.CreateSendLogsSqlRequest(logs, testModel.id));
            Assert.IsTrue(projectPage.CheckTheProjectSuccessfullyAdded(testModel), "Project should exist");
            Logger.Info("Step 5 completed.");

            projectPage.GoToTestPage(testModel);
            Assert.IsTrue(projectPage.testPage.State.WaitForDisplayed(), $"{projectPage.testPage.Name} shouldn't be presented");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(projectPage.testPage.GetProjectNameFromPage() == projectModel.name, "Names should be eqal");
                Assert.IsTrue(projectPage.testPage.CheckStatusOnPage(testModel), "Wrong status");
                Assert.IsTrue(projectPage.testPage.CheckEndTimeOnPage(testModel), "Wrong end time");
                Assert.IsTrue(projectPage.testPage.CheckDuration(testModel), "Wrong duration time");
                Assert.IsTrue(StringUtils.FormatLogs(logs).Contains(projectPage.testPage.GetLogsFromPage()), "Logs should be equal");
                Assert.IsTrue(screenshotString.Contains(projectPage.testPage.GetImgFromPage()), "Images should be equal");
                TestModel tessTestModelFromPage = projectPage.testPage.GeTestModelFromPage();
                Assert.IsTrue(tessTestModelFromPage.Equals(testModel), "Models should be time");
            });
            Logger.Info("Step 6 completed.");
        }
    }
}