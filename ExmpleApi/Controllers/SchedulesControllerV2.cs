using ExmpleApi.Models;
using ExmpleApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExmpleApi.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/schedules")]
    [ApiController]
    public class SchedulesControllerV2 : ControllerBase
    {
        private readonly IScheduleRepositoryV2 _repository;

        public SchedulesControllerV2(IScheduleRepositoryV2 repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return Ok(await _repository.GetSchedules());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _repository.GetSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(ScheduleV2 schedule)
        {
            var newSchedule = await _repository.AddSchedule(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { id = newSchedule.Id }, newSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, ScheduleV2 schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateSchedule(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _repository.DeleteSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
