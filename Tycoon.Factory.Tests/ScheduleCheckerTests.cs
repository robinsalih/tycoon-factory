namespace Tycoon.Factory.Tests
{
    public class ScheduleCheckerTests
    {
        private static readonly DateTimeOffset OneAm = new(2023, 10, 24, 01, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset FiveAm = new(2023, 10, 24, 05, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset SixAm = new(2023, 10, 24, 06, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset SevenAm = new(2023, 10, 24, 07, 00, 00, TimeSpan.Zero);
        private static readonly DateTimeOffset Noon = new(2023, 10, 24, 12, 00, 00, TimeSpan.Zero);

        private static readonly ActivityDefinition BuildComponent = new(1, "Build Component", false, TimeSpan.FromHours(2));
        private static readonly ActivityDefinition BuildMachine = new(2, "Build Machine", true, TimeSpan.FromHours(4));

        private static readonly Worker WorkerA = new(1, "A");
        private static readonly Worker WorkerB = new(2, "B");

        private readonly ScheduleChecker _sut = new();

        [Fact]
        private void ShouldReturnFalseIfNothingScheduled()
        {
            var assignments = Array.Empty<Assignment>();

            Assert.False(_sut.IsWorkerBusy(WorkerA, FiveAm, SixAm, assignments));
        }

        [Fact]
        private void ShouldReturnFalseIfAnotherWorkerIsBusy()
        {
            var workerBAssignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerB });
            var assignments = new[] { workerBAssignment };

            Assert.False(_sut.IsWorkerBusy(WorkerA, OneAm, FiveAm, assignments));
        }

        [Fact]
        private void ShouldReturnTrueIfWorkerIsBusy()
        {
            var workerAAssignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerA });
            var assignments = new[] { workerAAssignment };

            Assert.True(_sut.IsWorkerBusy(WorkerA, OneAm, FiveAm, assignments));
        }

        [Fact]
        private void ShouldReturnTrueIfWorkerIsRecharging()
        {
            var workerAAssignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerA });
            var assignments = new[] { workerAAssignment };

            Assert.True(_sut.IsWorkerBusy(WorkerA, SixAm, SevenAm, assignments));
        }
        [Fact]
        private void ShouldReturnFalseIfNoOverlap()
        {
            var workerAAssignment = new Assignment(1, BuildComponent, OneAm, FiveAm, new[] { WorkerA });
            var assignments = new[] { workerAAssignment };

            Assert.False(_sut.IsWorkerBusy(WorkerA, SevenAm, Noon, assignments));
        }

        [Fact]
        private void ShouldReturnTrueForMultipleWorkerAssignment()
        {
            var multipleAssignment = new Assignment(1, BuildMachine, OneAm, FiveAm, new[] { WorkerA, WorkerB });
            var assignments = new[] { multipleAssignment };

            Assert.True(_sut.IsWorkerBusy(WorkerB, OneAm, FiveAm, assignments));
        }

        [Fact]
        private void ShouldReturnFalseForMultipleAssignmentsOnDifferentDays()
        {
            var assignment1 = new Assignment(1, BuildComponent, OneAm - TimeSpan.FromDays(-1), FiveAm - TimeSpan.FromDays(-1), new[] { WorkerB });
            var assignment2 = new Assignment(1, BuildComponent, OneAm + TimeSpan.FromDays(-1), FiveAm + TimeSpan.FromDays(-1), new[] { WorkerB });
            var assignments = new[] { assignment1, assignment2 };

            Assert.False(_sut.IsWorkerBusy(WorkerB, OneAm, FiveAm, assignments));
        }
    }
}