using Aquality.Selenium.Core.Configurations;
using ExamTaskDockerUiDb.Constants;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Aquality.Selenium.Browsers;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class FileReader
    {
        public static Dictionary<string, string> GetDataFromJson(string path)
        {
            LoggerUtils.LogStep(nameof(GetDataFromJson) + " \"Get data from json\"");
            var json = File.ReadAllText(path);
            var jsonObj = JObject.Parse(json);
            Dictionary<string, string> methods = new Dictionary<string, string>();

            foreach (var element in jsonObj)
            {
                methods.Add(element.Key, element.Value.ToString());
            }
            return methods;
        }

        public static string ConvertToBase64(byte[] imageArray)
        {
            LoggerUtils.LogStep(nameof(ConvertToBase64) + $" \"Start convertation byte[] to string\"");
            return Convert.ToBase64String(imageArray);
        }

        public static void ClearLogFile()
        {
            FileInfo file = new FileInfo(FileConstants.PathToLogFile);

            if (file.Exists)
            {
                file.Delete();
                LoggerUtils.LogStep(nameof(ClearLogFile) + $" \"Log file deleted - [{file}]\"");
            }
        }

        public static string GetProjectName()
        {
            LoggerUtils.LogStep(nameof(GetProjectName) + " \"Get project name\"");
            StackFrame sf = new StackFrame();
            string className = sf.GetMethod().DeclaringType.ToString();
            return StringUtils.SeparateString(className, '.')[0];
        }

        public static string GetTestMethodName()
        {
            LoggerUtils.LogStep(nameof(GetProjectName) + " \"Get test method name\"");
            StackFrame sf = new StackFrame();
            string methodName = sf.GetMethod().ToString();
            return StringUtils.SeparateString(methodName, ' ')[0];
        }

        public static string GetBrowserName()
        {
            var settingsFile = AqualityServices.Get<ISettingsFile>();
            return settingsFile.GetValue<string>(".browserName");
        }

        public static ByteArrayContent ReadImage(string path)
        {
            LoggerUtils.LogStep(nameof(ReadImage) + $" \"Image - [{path}] read\"");
            byte[] imgdata = File.ReadAllBytes(path);
            return new ByteArrayContent(imgdata);
        }

        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                LoggerUtils.LogStep(nameof(ReadFile) + $" \"File - [{path}] read\"");
                return sr.ReadToEnd();
            }
        }

        public static void SaveScreenshotAsPng(byte[] screenshot)
        {
            Image image = Image.FromStream(new MemoryStream(screenshot));
            image.Save(FileConstants.PathToScreenshot + "screenshot.png", ImageFormat.Png);
        }
    }
}