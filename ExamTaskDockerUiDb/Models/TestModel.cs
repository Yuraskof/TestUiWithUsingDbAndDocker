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
    }
}
