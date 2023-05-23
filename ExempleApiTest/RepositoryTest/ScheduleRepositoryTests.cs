using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExmpleApi.Data;
using ExmpleApi.Models;
using ExmpleApi.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ExempleApiTest.RepositoryTest
{
    public class ScheduleRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;
        private ScheduleRepository _repository;

        public ScheduleRepositoryTests()
        {
            // Initialize the in-memory database
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ScheduleTestDatabase")
                .Options;

            // Create context
            _context = new ApplicationDbContext(_options);

            // Create repository
            _repository = new ScheduleRepository(_context);
        }

        [Fact]
        public async Task AddSchedule_ShouldReturnSchedule_WhenAdded()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddScheduleTestDb")
                .Options;

            var schedule = new Schedule {Id = 1, EmployeeId = 101, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new ScheduleRepository(context);
                await repository.AddSchedule(schedule);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Schedules.Count());
                Assert.Equal(1, context.Schedules.Single().Id);
            }
        }

        [Fact]
        public async Task GetSchedules_ShouldReturnSchedules_WhenExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSchedulesTestDb")
                .Options;

            var schedule1 = new Schedule { Id = 1, EmployeeId = 101, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            var schedule2 = new Schedule { Id = 2, EmployeeId = 102, Start = DateTime.Now, End = DateTime.Now.AddHours(2) };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                await context.Schedules.AddRangeAsync(schedule1, schedule2);
                await context.SaveChangesAsync();

                var repository = new ScheduleRepository(context);
                var result = await repository.GetSchedules();

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetSchedule_ShouldReturnSchedule_WhenExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetScheduleTestDb")
                .Options;

            var schedule = new Schedule { Id = 1, EmployeeId = 101, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                await context.Schedules.AddAsync(schedule);
                await context.SaveChangesAsync();

                var repository = new ScheduleRepository(context);
                var result = await repository.GetSchedule(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
            }
        }

        [Fact]
        public async Task UpdateSchedule_ShouldUpdateSchedule_WhenExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateScheduleTestDb")
                .Options;

            var schedule = new Schedule { Id = 1, EmployeeId = 101, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                await context.Schedules.AddAsync(schedule);
                await context.SaveChangesAsync();

                var repository = new ScheduleRepository(context);
                schedule.End = schedule.Start.AddHours(2);
                await repository.UpdateSchedule(schedule);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(2, context.Schedules.Single().End.Hour - context.Schedules.Single().Start.Hour);
            }
        }

        [Fact]
        public async Task DeleteSchedule_ShouldRemoveSchedule_WhenExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteScheduleTestDb")
                .Options;

            var schedule = new Schedule { Id = 1, EmployeeId = 101, Start = DateTime.Now, End = DateTime.Now.AddHours(1) };

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                await context.Schedules.AddAsync(schedule);
                await context.SaveChangesAsync();

                var repository = new ScheduleRepository(context);
                await repository.DeleteSchedule(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(0, context.Schedules.Count());
            }
        }




    }


}
