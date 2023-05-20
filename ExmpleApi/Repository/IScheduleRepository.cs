using ExmpleApi.Models;

namespace ExmpleApi.Repository
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetSchedules();
        Task<Schedule?> GetSchedule(int id);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Schedule> UpdateSchedule(Schedule schedule);
        Task<Schedule?> DeleteSchedule(int id);
    }
}
