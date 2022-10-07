using ExamTaskDockerUiDb.Utilities;

namespace ExamTaskDockerUiDb.Models
{
    public class TestModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status_id { get; set; }
        public string method_name { get; set; }
        public int session_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string env { get; set; }
        public string browser { get; set; }
        public int project_id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            TestModel other = (TestModel)obj;

            if (name.Equals(other.name) && method_name.Equals(other.method_name) &&
                start_time.Equals(other.start_time) && env.Equals(other.env) && browser.Equals(other.browser))
            {
                LoggerUtils.LogStep(nameof(Equals) + " \"Test models are equal\"");
                return true;
            }
            LoggerUtils.LogStep(nameof(Equals) + " \"Test models are not equal\"");
            return false;
        }
    }
}
