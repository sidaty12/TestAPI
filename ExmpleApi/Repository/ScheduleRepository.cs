using ExmpleApi.Data;
using ExmpleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExmpleApi.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public ScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Schedule>> GetSchedules()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<Schedule?> GetSchedule(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            var result = await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule?> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            return schedule;
        }
    }
}
