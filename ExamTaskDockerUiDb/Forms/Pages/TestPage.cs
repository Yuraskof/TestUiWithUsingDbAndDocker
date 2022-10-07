using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Aquality.Selenium.Forms;
using ExamTaskDockerUiDb.Models;
using Aquality.Selenium.Elements.Interfaces;
using ExamTaskDockerUiDb.Utilities;
using Humanizer;

namespace ExamTaskDockerUiDb.Forms.Pages
{
    public class TestPage : Form
    {
        private ITextBox ProjectNameTextBox => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Project name\")]//following-sibling::p"), "Project name text box");
        private ITextBox TestNameTextBox => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Test name\")]//following-sibling::p"), "Test name text box");
        private ITextBox TestMethodNameTextBox => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Test method name\")]//following-sibling::p"), "Test method name text box");
        private ITextBox StatusTextBox(string status) => ElementFactory.GetTextBox(By.XPath(string.Format("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Status\")]//following-sibling::p//span[contains (text(), \"{0}\")]", status)), "Status text box");
        private ITextBox StartTime => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Time info\")]//following-sibling::p[contains (text(), \"Start time\")]"), "Start time text box");
        private ITextBox Duration => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Time info\")]//following-sibling::p[contains (text(), \"Duration\")]"), "Duration text box");
        private ITextBox EndTime => ElementFactory.GetTextBox(By.XPath("//h4[@class= \"list-group-item-heading\"][contains (text(), \"Time info\")]//following-sibling::p[contains (text(), \"End time\")]"), "End time text box");


        private TestModel testModel;

        
        public TestPage() : base(By.XPath("//div[contains (@class, \"fail-reason-block\")]"), "Test page")
        {
            testModel = new TestModel();
        }

        public void GeTestModelFromPage() //TestModel
        {
            SetTestNameFromPage();
            SetTestMethodFromPage();
            SetStartTimeFromPage();

            //return testModel;
        }

        public string GetProjectNameFromPage()
        {
            return ProjectNameTextBox.GetText();
        }

        private void SetTestNameFromPage()
        {
            testModel.name = TestNameTextBox.GetText();
        }

        private void SetTestMethodFromPage()
        {
            testModel.method_name = TestMethodNameTextBox.GetText();
        }

        public bool CheckStatusOnPage(TestModel model)
        {
            string status = null;
            if (model.status_id == "")
            {
                status = "In progress";
            }
            return StatusTextBox(status).State.WaitForEnabled();
        }
        private void SetStartTimeFromPage()
        {
            string date = StartTime.GetText();
            Regex regex = new Regex(": ");
            date = regex.Replace(date, "!");
            date = StringUtils.SeparateString(date,'!')[1];
            date = StringUtils.ConvertDateTime(date);
            testModel.start_time = date;
        }

        public bool CheckDuration(TestModel model)
        {
            string duration = Duration.GetText();

            if (model.status_id == "")
            {
                return duration.Contains("00:00:00.000");
            }
            return false;
        }

        public bool CheckEndTimeOnPage(TestModel model)
        {
            string endTime = EndTime.GetText();

            if (model.status_id == "")
            {
                var endTimeStrings = StringUtils.SeparateString(endTime, ':');
                return endTimeStrings.Count == 1;
            }
            return false;
        }
    }
}
