namespace ExamTaskDockerUiDb.Utilities
{
    public class ApiUtils
    {
        public static HttpResponseMessage PostRequest(string request, HttpContent content) 
        {
            LoggerUtils.LogStep(nameof(PostRequest) + $" \"Post request - [{request}]\"");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.PostAsync(request, content).Result;
            client.Dispose();
            return response;
        }
    }
}
