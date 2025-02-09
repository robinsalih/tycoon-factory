namespace Tycoon.Factory.Tests
{
    public class WorkerRepositoryTests
    {
        private readonly WorkerRepository _sut = new();

        [Fact]
        public async Task ShouldCreateAndGetSingle()
        {
            var createResult = await _sut.CreateWorker("Foo");
            var getResult = await _sut.GetWorker(createResult.Id);
            Assert.Equal(createResult, getResult);
        }

        [Fact]
        public async Task ShouldCreateAndGetMultiple()
        {
            var createResult1 = await _sut.CreateWorker("Foo");
            var createResult2 = await _sut.CreateWorker("Bar");
            var getResults = (await _sut.GetAllWorkers()).ToImmutableDictionary(r => r.Id);

            Assert.Equal(2, getResults.Count());
            Assert.Equal(createResult1, getResults[createResult1.Id]);
            Assert.Equal(createResult2, getResults[createResult2.Id]);
        }

        [Fact]
        public async Task ShouldCreateAndModify()
        {
            var createResult = await _sut.CreateWorker("Foo");
            var modifiedWorker = new Worker(createResult.Id, "Bar");
            await _sut.ModifyWorker(modifiedWorker);
            var getResult = await _sut.GetWorker(createResult.Id);
            Assert.Equal(modifiedWorker, getResult);

        }

        [Fact]
        public async Task ShouldReturnNullIfNotFound()
        {
            Assert.Null(await _sut.GetWorker(123));
        }

        [Fact]
        public async Task ShouldDelete()
        {
            var createResult = await _sut.CreateWorker("Foo");
            await _sut.DeleteWorker(createResult.Id);
            Assert.Null(await _sut.GetWorker(createResult.Id));
        }
    }
}