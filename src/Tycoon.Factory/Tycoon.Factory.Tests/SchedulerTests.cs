using Moq;
using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Model;
using Tycoon.Factory.Core.Scheduling;

namespace Tycoon.Factory.Tests
{

    public class SchedulerTests
    {
        private readonly Scheduler _sut;
        private readonly Mock<IWorkerRepository> _workerRepoMock = new();
        private readonly Mock<IActivityRepository> _activityRepoMock = new();
        private readonly Mock<IAssignmentRepository> _assignmentRepoMock = new();
        private readonly Mock<IScheduleChecker> _checker = new();

        private static readonly DateTimeOffset OneAm = new(2023, 10, 24, 01, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset FiveAm = new(2023, 10, 24, 05, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset SevenAm = new(2023, 10, 24, 07, 00, 00, TimeSpan.Zero);

        private static readonly ActivityDefinition BuildComponent = new(1, "Build Component", false, TimeSpan.FromHours(2));
        private static readonly ActivityDefinition BuildMachine = new(2, "Build Machine", true, TimeSpan.FromHours(4));

        private static readonly Worker WorkerA = new(1, "A");
        private static readonly Worker WorkerB = new(2, "B");

        private static readonly Assignment[] ExistingAssignments = new[] { new Assignment(1, BuildComponent, OneAm, SevenAm, new[] { WorkerA, WorkerB }) };

        public SchedulerTests()
        {
            _activityRepoMock.Setup(x => x.GetActivityDefinition(1)).ReturnsAsync(BuildComponent);
            _activityRepoMock.Setup(x => x.GetActivityDefinition(2)).ReturnsAsync(BuildMachine);
            _workerRepoMock.Setup(x => x.GetWorker(1)).ReturnsAsync(WorkerA);
            _workerRepoMock.Setup(x => x.GetWorker(2)).ReturnsAsync(WorkerB);
            _assignmentRepoMock.Setup(x => x.GetAllAssignments()).ReturnsAsync(ExistingAssignments);

            _sut = new(_assignmentRepoMock.Object, _workerRepoMock.Object, _activityRepoMock.Object, _checker.Object);
        }

        [Fact]
        public async Task ShouldThrowExceptionIfActivityDoesNotExist()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ScheduleActivity(99, OneAm, FiveAm, new[] {1}));
        }

        [Fact]
        public async Task ShouldThrowExceptionIfStartTimeAfterEndTime()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ScheduleActivity(1, FiveAm, OneAm, new[] {1}));
        }

        [Fact]
        public async Task ShouldThrowExceptionIfWorkerDoesNotExist()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ScheduleActivity(1, OneAm, FiveAm, new[] { 99 }));
        }

        [Fact]
        public async Task ShouldThrowExceptionIfNoWorkersSpecified()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ScheduleActivity(1, OneAm, FiveAm, Array.Empty<int>()));
        }

        [Fact]
        public async Task ShouldThrowExceptionIfMultipleWorkersAssignedToSingleWorkerActivity()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ScheduleActivity(1, OneAm, FiveAm, new[] { 1, 2 }));
        }

        [Fact]
        public async Task ShouldAssignSingleWorkerActivity()
        {
            var assignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerA });
            _assignmentRepoMock.Setup(x => x.CreateAssignment(1, OneAm, FiveAm, new[] { 1 })).ReturnsAsync(assignment);

            var result = await _sut.ScheduleActivity(1, OneAm, FiveAm, new[] { 1 });

            Assert.Equal(assignment, result);
        }

        [Fact]
        public async Task ShouldAssignMultipleWorkerActivity()
        {
            var assignment = new Assignment(1, BuildMachine, OneAm, FiveAm, new[] { WorkerA, WorkerB });
            _assignmentRepoMock.Setup(x => x.CreateAssignment(2, OneAm, FiveAm, new[] { 1, 2 })).ReturnsAsync(assignment);

            var result = await _sut.ScheduleActivity(2, OneAm, FiveAm, new[] { 1, 2 });

            Assert.Equal(assignment, result);
        }

        [Fact]
        public async Task ShouldConvertAllTimesToUTC()
        {
            var twoPmOneHourOffset = new DateTimeOffset(2023, 10, 24, 14, 00, 00, TimeSpan.FromHours(1));
            var onePmNoOffset = new DateTimeOffset(2023, 10, 24, 13, 00, 00, TimeSpan.Zero);
            var assignment = new Assignment(2, BuildMachine, OneAm, onePmNoOffset, new[] { WorkerA, WorkerB });
            _assignmentRepoMock.Setup(x => x.CreateAssignment(2, OneAm, onePmNoOffset, new[] { 1, 2 })).ReturnsAsync(assignment);
            
            var result = await _sut.ScheduleActivity(2, OneAm, twoPmOneHourOffset, new[] { 1, 2 });
            
            Assert.Equal(assignment, result);
        }

        [Fact]
        public async Task ShouldNotCreateTaskIfWorkerBusy()
        {
            var assignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerA });
            _assignmentRepoMock.Setup(x => x.CreateAssignment(1, OneAm, FiveAm, new[] { 1 })).ReturnsAsync(assignment);
            _checker.Setup(x => x.IsWorkerBusy(WorkerA, OneAm, SevenAm, ExistingAssignments)).Returns(true);
            
            await Assert.ThrowsAsync<WorkerBusyException>(() => _sut.ScheduleActivity(1, OneAm, FiveAm, new[] { 1 }));
        }
    }
}