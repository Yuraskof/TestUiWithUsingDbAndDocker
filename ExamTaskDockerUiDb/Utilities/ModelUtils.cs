using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Constants;
using ExamTaskDockerUiDb.Models.RequestModels;
using ExamTaskDockerUiDb.Models.ResponseModels;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class ModelUtils
    {
        public static UploadImageResponseModel uploadImageResponse = new UploadImageResponseModel();
        
        public static GetLikesRequestModel CreateGetLikesRequestModel(object content)
        {
            LoggerUtils.LogStep(nameof(CreateGetLikesRequestModel) + " \"Start creating get likes request model\"");
            GetLikesRequestModel model = new GetLikesRequestModel();
            model.v = BaseTest.testData.ApiVersion;
            model.owner_id = BaseTest.testData.UserId;
            model.access_token = BaseTest.testData.Token;
            model.post_id = content.ToString();
            return model;
        }

        public static bool FindLikeFromUser(GetLikesResponseModel getLikesResponseModel, WallPostModel postModel)
        {
            LoggerUtils.LogStep(nameof(FindLikeFromUser) + " \"Start searching like from desired user\"");

            foreach (var user in getLikesResponseModel.response.users)
            {
                if (user.uid == postModel.owner_id)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
