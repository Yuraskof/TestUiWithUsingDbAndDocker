using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamTaskDockerUiDb.Forms
{
    public class AddProjectForm : Form
    {
        private ITextBox ProjectNameTextBox => ElementFactory.GetTextBox(By.XPath("//input[@id = \"projectName\"]"), "Project name");
        private IButton SubmitButton => ElementFactory.GetButton(By.XPath("//button[@type = \"submit\"]"), "Submit button");
        private ITextBox SuccessTextBox => ElementFactory.GetTextBox(By.XPath("//div[contains(@class, \"alert-success\")]"), "Success add message");
        private ILink ScriptLink => ElementFactory.GetLink(By.XPath("//scpipt[@src = \"/web/resources/js/closeModal.js\"]"), "Close popUp script");

        public AddProjectForm() : base(By.XPath("//form[@id = \"addProjectForm\"]"), "Add project form") ////div[@class = "modal-body"]
        {
        }

        public void AddProject(string name)
        { 
            EnterName(name);
            //SubmitButton.JsActions.SetAttribute("onclick", "window.close()");
            SubmitButton.Click();
            SuccessTextBox.State.WaitForEnabled();
        }

        private void EnterName(string name)
        {
            ProjectNameTextBox.State.WaitForEnabled();
            ProjectNameTextBox.ClearAndType(name);
        }

        public void CloseAddProjectForm()
        {
            //AqualityServices.Browser.GoTo(ScriptLink.Href);

            //SuccessTextBox.JsActions.SetAttribute("onclick", "window.close()");
            //AqualityServices.Browser.Driver.

            //PinnedScript pinnedScript = ("function closePopUp() {\r\n    $('#addProject').modal('hide')\r\n}");


            //SuccessTextBox.Click();

            //AqualityServices.Browser.Driver.SwitchTo().DefaultContent();

            //////AqualityServices.Browser.Driver.ExecuteScript("closePopUp()", );

            //FormElement.JsActions.ExecuteScript("function closePopUp() {\r\n    $('#addProject').modal('hide')\r\n}");


            //AqualityServices.Browser.Driver.ExecuteScript("function closePopUp() {\r\n    $('#addProject').modal('hide')\r\n}");




            //FooterTextBox.JsActions.Click();
            //AqualityServices.Browser.Driver.ExecuteScript("('#iframe id=\"addProjectFrame\").dialog('close');"); //arguments[0].closePopUp(); window.close();  $('#iframeId').dialog('close');  "javascript:ClosePopup(true);" $('#iframe id="addProjectFrame").dialog('close');
        }
        
        //protected override IDictionary<string, IElement> ElementsForVisualization => new Dictionary<string, IElement>()
        //{
        //    {"PostImage", PostImageFullSize },
        //};

        //public float CompareImages()
        //{
        //    PostImageFullSize.State.WaitForEnabled(BaseTest.timeoutForElements);
        //    return Dump.Compare();
        //}



    }
}
