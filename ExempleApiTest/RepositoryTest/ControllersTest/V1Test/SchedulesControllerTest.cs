using ExmpleApi.Controllers.V1;
using ExmpleApi.Models;
using ExmpleApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExmpleApi.Test
{
    public class SchedulesControllerTests
    {
        private readonly Mock<IScheduleRepository> _mockRepo;
        private readonly SchedulesController _controller;

        public SchedulesControllerTests()
        {
            _mockRepo = new Mock<IScheduleRepository>();
            _controller = new SchedulesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSchedules_ReturnsOkResult()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetSchedules()).ReturnsAsync(GetTestSchedules());

            // Act
            var result = await _controller.GetSchedules();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var schedules = Assert.IsAssignableFrom<IEnumerable<Schedule>>(okResult.Value);
        }

        [Fact]
        public async Task GetSchedule_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetSchedule(0)).ReturnsAsync((Schedule)null);

            // Act
            var result = await _controller.GetSchedule(0);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostSchedule_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newSchedule = new Schedule
            {
                Id = 1,
                EmployeeId = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1)
            };
            _mockRepo.Setup(repo => repo.AddSchedule(newSchedule)).ReturnsAsync(newSchedule);

            // Act
            var result = await _controller.PostSchedule(newSchedule);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedSchedule = Assert.IsType<Schedule>(actionResult.Value);
            Assert.Equal(1, returnedSchedule.Id);
        }

        // Add similar tests for PutSchedule and DeleteSchedule here

        private List<Schedule> GetTestSchedules()
        {
            var schedules = new List<Schedule>
            {
                new Schedule{ Id=1, EmployeeId=1 },
                new Schedule{ Id=2, EmployeeId=2 },
                new Schedule{ Id=3, EmployeeId=3 },
            };

            return schedules;
        }

        [Fact]
        public async Task PutSchedule_ReturnsBadRequest_ForMismatchedId()
        {
            // Arrange
            var testSchedule = new Schedule { Id = 1, EmployeeId = 1, Start = DateTime.Now, End = DateTime.Now.AddDays(1) };
            _mockRepo.Setup(repo => repo.UpdateSchedule(testSchedule)).ReturnsAsync(testSchedule);

            // Act
            var result = await _controller.PutSchedule(2, testSchedule); // Here we are passing id as 2 but in schedule object id is 1, so it's mismatched id.

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteSchedule_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteSchedule(0)).ReturnsAsync((Schedule)null);

            // Act
            var result = await _controller.DeleteSchedule(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
