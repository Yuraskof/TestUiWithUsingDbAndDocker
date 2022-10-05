using ExamTaskDockerUiDb.Base;
using ExamTaskDockerUiDb.Models;
using ExamTaskDockerUiDb.Models.RequestModels;

namespace ExamTaskDockerUiDb.Utilities
{
    public static class ModelUtils
    {
        public static GetAccessTokenModel CreateGetAccessTokenModel()
        {
            LoggerUtils.LogStep(nameof(CreateGetAccessTokenModel) + " \"Start creating get access token model\"");
            GetAccessTokenModel model = new GetAccessTokenModel();
            model.variant = BaseTest.testData.Variant;
            return model;
        }

        public static bool CheckModelsDates(List<TestModel> modelsFromPage)
        {
            LoggerUtils.LogStep(nameof(CheckModelsDates) + " \"Start checking tests sort\"");
            string previousTestStartTime = null;

            for (int i = 0; i < modelsFromPage.Count; i++)
            {
                if (previousTestStartTime == null)
                {
                    previousTestStartTime = modelsFromPage[i].start_time;
                    continue;
                }

                if (Convert.ToDateTime(previousTestStartTime) < Convert.ToDateTime(modelsFromPage[i].start_time))
                {
                    LoggerUtils.LogStep(nameof(CheckModelsDates) + $" \"The latest date from previous - [{modelsFromPage[i]}]\"");
                    return false;
                }
                previousTestStartTime = modelsFromPage[i].start_time;
            }
            return true;
        }

        public static bool CompareModels(List<TestModel> modelsFromPage, List<TestModel> modelsFromDb)
        {
            LoggerUtils.LogStep(nameof(CompareModels) + " \"Start comparing models from database and from the page\"");
            int matchesCount = 0;

            for (int i = 0; i < modelsFromPage.Count; i++)
            {
                foreach (var dbModel in modelsFromDb)
                {
                    dbModel.start_time = StringUtils.ConvertDateTime(dbModel.start_time);
                    modelsFromPage[i].start_time = StringUtils.ConvertDateTime(modelsFromPage[i].start_time);

                    if (dbModel.name == modelsFromPage[i].name && dbModel.start_time == modelsFromPage[i].start_time)
                    {
                        matchesCount++;
                        modelsFromDb.Remove(dbModel);
                        break;
                    }
                }
            }

            if (matchesCount == modelsFromPage.Count)
            {
                return true;
            }
            return false;
        }
    }
}
