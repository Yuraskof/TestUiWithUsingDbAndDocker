using System.Text;
using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Models.RequestModels;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class ApiApplicationRequest
    {
        public static Dictionary<string, string> apiMethods = FileReader.GetDataFromJson(FileConstants.PathToApiMethods);
        private static readonly string host = BaseTest.testData.ApiHost;


        public static string GetAccessToken(GetAccessTokenModel model)
        {
            LoggerUtils.LogStep(nameof(GetAccessToken) + " \"Send request to get access token\"");

            string request = host + apiMethods["getToken"] + "?" + "variant=" + model.variant;

            var stringContent = JsonUtils.SerializeJsonData(model);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, FileConstants.MediaType);
            HttpResponseMessage response = ApiUtils.PostRequest(request, httpContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(GetAccessToken) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            return response.Content.ReadAsStringAsync().Result;
        }

        public static bool CheckStatusCode(int expectedStatusCode, HttpResponseMessage response)
        {
            LoggerUtils.LogStep(nameof(CheckStatusCode) + " \"Check status code\"");
            return (int)response.StatusCode == expectedStatusCode;
        }
    }
}
