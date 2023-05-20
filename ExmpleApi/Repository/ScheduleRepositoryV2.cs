using ExmpleApi.Data;
using ExmpleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExmpleApi.Repository
{
    public class ScheduleRepositoryV2 : IScheduleRepositoryV2
    {
        private readonly ApplicationDbContext _context;

        public ScheduleRepositoryV2(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScheduleV2>> GetSchedules()
        {
            return await _context.SchedulesV2.ToListAsync();

        }

        public async Task<ScheduleV2?> GetSchedule(int id)
        {
            return  await _context.SchedulesV2.FindAsync(id);

        }

        public async Task<ScheduleV2> AddSchedule(ScheduleV2 schedule)
        {
            var result = await _context.SchedulesV2.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ScheduleV2> UpdateSchedule(ScheduleV2 schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<ScheduleV2?> DeleteSchedule(int id)
        {
            var schedule   = await _context.SchedulesV2.FindAsync(id);
            if (schedule != null)
            {
                _context.SchedulesV2.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            return schedule;
        }
    }
}
