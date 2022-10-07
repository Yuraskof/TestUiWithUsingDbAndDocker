namespace ExamTaskDockerUiDb.Models
{
    public class SessionModel
    {
        public int id { get; set; }
        public string session_key { get; set; }
        public string created_time { get; set; }
        public int build_number { get; set; }
    }
}
