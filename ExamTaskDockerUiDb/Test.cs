using Aquality.Selenium.Browsers;
using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Forms.Pages;
using ExamTaskDockerUiDb.Models;
using ExamTaskDockerUiDb.Models.RequestModels;
using ExamTaskDockerUiDb.Utilities;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Forms;
using System.IO;
using System.Text;

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

            //allProjectsPage.GoToProjectPage(testData.ProjectName);
            //ProjectPage projectPage = new ProjectPage();
            //Assert.IsTrue(projectPage.State.WaitForDisplayed(), $"{projectPage.Name} should be presented");
            //List<TestModel> testModelsFromPage = projectPage.GetTestsNames();
            //List<TestModel> testModelsFromDb = ResponseParser.ParseToTestModel(DataBaseUtils.SendRequest(sqlRequests["projectId1Tests"]));

            //Assert.Multiple(() =>
            //{
            //    Assert.IsTrue(ModelUtils.CheckModelsDates(testModelsFromPage), "Dates should be descending");
            //    Assert.IsTrue(ModelUtils.CompareModels(testModelsFromPage, testModelsFromDb), "Values should be equal");
            //});
            //Logger.Info("Step 3 completed.");

            //AqualityServices.Browser.GoBack();
            Assert.IsTrue(allProjectsPage.State.WaitForDisplayed(), $"{allProjectsPage.Name} should be presented");
            allProjectsPage.OpenAddProjectForm();
            Assert.IsTrue(allProjectsPage.addProjectForm.State.WaitForDisplayed(), $"{allProjectsPage.addProjectForm.Name} should be presented");
            //allProjectsPage.addProjectForm.AddProject(FileReader.GetProjectName());
            allProjectsPage.CloseAddProjectForm();
            Assert.IsTrue(allProjectsPage.addProjectForm.State.WaitForNotDisplayed(), $"{allProjectsPage.addProjectForm.Name} shouldn't be presented");
            AqualityServices.Browser.Refresh();
            Assert.IsTrue(allProjectsPage.ProjectIsPresented(FileReader.GetProjectName()), "Project should be presented");
            Logger.Info("Step 4 completed.");

            allProjectsPage.GoToProjectPage(FileReader.GetProjectName());
            string projectId = ResponseParser.ParseToString(DataBaseUtils.SendRequest(StringUtils.CreateGetProjectIdByNameRequest(FileReader.GetProjectName())));
            DataBaseUtils.SendRequest(StringUtils.CreateSessionSqlRequest());
            string sessionId = ResponseParser.ParseToString(DataBaseUtils.SendRequest(StringUtils.CreateGetSessionIdRequest(BaseTest.sessionID)));
            StackFrame sf = new StackFrame();
            string methodName = StringUtils.SeparateString(sf.GetMethod().ToString(), ' ')[1];
            DataBaseUtils.SendRequest(StringUtils.CreateSendTestSqlRequest(methodName, Convert.ToInt32(projectId), Convert.ToInt32(sessionId)));
            string testId = ResponseParser.ParseToString(DataBaseUtils.SendRequest(StringUtils.CreateGetTestIdSqlRequest()));
            byte[] screenshot = AqualityServices.Browser.GetScreenshot();
            FileReader.SaveScreenshotAsPng(screenshot);
            string screenshotString = FileReader.ConvertToBase64(screenshot);
            DataBaseUtils.SendRequest(StringUtils.CreateSendAttachmentsSqlRequest(screenshotString, testData.ImageContentType, Convert.ToInt32(testId)));
            string logs;

            using (StreamReader streamReader = new StreamReader(FileConstants.PathToLogFile, Encoding.UTF8))
            {
                logs = streamReader.ReadToEnd();
                logs.Replace("\"", "'");
            }
            DataBaseUtils.SendRequest(StringUtils.CreateSendLogsSqlRequest(logs, Convert.ToInt32(testId)));







            //List<ProjectModel> projects = ResponseParser.ParseToProjectModel(DataBaseUtils.SendRequest(sqlRequests["getProjectInfoByName"]));
            //int newProjectId = ModelUtils.FindProjectIdByName(FileReader.GetProjectName(), projects);

        }
    }
}