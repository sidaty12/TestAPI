using ExmpleApi.Models;

namespace ExmpleApi.Repository
{
    public interface IScheduleRepositoryV2
    {
        Task<IEnumerable<ScheduleV2>> GetSchedules();
        Task<ScheduleV2?> GetSchedule(int id);
        Task<ScheduleV2> AddSchedule(ScheduleV2 schedule);
        Task<ScheduleV2> UpdateSchedule(ScheduleV2 schedule);
        Task<ScheduleV2?> DeleteSchedule(int id);
    }
}
