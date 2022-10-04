using System.Text;
using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Constants;

namespace ExamTaskDockerUiDb.Utilities
{
    public class ApiApplicationRequest
    {
        public static Dictionary<string, string> apiMethods = FileReader.GetMethods(FileConstants.PathToApiMethods);
        private static string host = BaseTest.testData.ApiHost;

        public static WallPostResponseModel CreatePostOnTheWall(WallPostModel model)
        {
            LoggerUtils.LogStep(nameof(CreatePostOnTheWall) + " \"Send post\"");
            
            string request = host + apiMethods["postOnTheWall"] + "?"+ "owner_id=" + model.owner_id + "&" +
                             "message="+model.message + "&" + "access_token=" + model.access_token + "&" + "v=" +
                             model.v;

            var stringContent = JsonUtils.SerializeJsonData(model);
            var httpContent = new StringContent(stringContent, Encoding.UTF8, FileConstants.MediaType);
            HttpResponseMessage response = ApiUtils.PostRequest(request, httpContent);

            if (!CheckStatusCode(StatusCodes.OK, response))
            {
                LoggerUtils.LogStep(nameof(CreatePostOnTheWall) + $" \"Invalid status code - [{response.StatusCode}]\"");
                return null;
            }
            string contentString = response.Content.ReadAsStringAsync().Result;
            return JsonUtils.ReadJsonData<WallPostResponseModel>(contentString);
        }


        public static bool CheckStatusCode(int expectedStatusCode, HttpResponseMessage response)
        {
            LoggerUtils.LogStep(nameof(CheckStatusCode) + " \"Check status code\"");
            return (int)response.StatusCode == expectedStatusCode;
        }
    }
}
