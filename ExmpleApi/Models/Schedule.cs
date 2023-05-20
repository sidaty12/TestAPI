namespace ExmpleApi.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
