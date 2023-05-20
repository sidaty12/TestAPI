namespace ExmpleApi.Models
{
    public class ScheduleV2
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
