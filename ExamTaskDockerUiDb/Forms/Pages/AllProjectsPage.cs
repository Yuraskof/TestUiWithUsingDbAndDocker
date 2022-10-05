using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamTaskDockerUiDb.Forms.Pages
{
    public class AllProjectsPage : Form
    {
        private ITextBox FooterTextBox => ElementFactory.GetTextBox(By.XPath("//p[contains(@class, \"footer-text\")]//span"), "Footer text box");
        private IButton ProjectButton(string project) => ElementFactory.GetButton(By.XPath(string.Format("//div[@class=\"list-group\"]//a[contains(text(), \"{0}\")]", project)), "Project button");
        
        public AllProjectsPage() : base(By.XPath("//div[contains (@class, \"panel-default\")]//div[contains (text(), \"projects\")]"), "Projects page")
        {
        }
        
        public string GetFooterText()
        {
            FooterTextBox.State.WaitForEnabled();
            return FooterTextBox.GetText();
        }

        public void GoToProjectPage(string project)
        {
            ProjectButton(project).State.WaitForEnabled();
            ProjectButton(project).Click();
        }
    }
}