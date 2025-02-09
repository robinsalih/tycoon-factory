using Tycoon.Factory.Core.Interfaces;

public class PopulateData
{
    private readonly IActivityRepository _activityRepository;
    private readonly IWorkerRepository _workerRepository;

    public PopulateData(IActivityRepository activityRepository, IWorkerRepository workerRepository)
    {
        _activityRepository = activityRepository;
        _workerRepository = workerRepository;
    }

    public async Task PopulateRepos()
    {
        await _activityRepository.CreateActivityDefinition("BuildCompnent", false, 2);
        await _activityRepository.CreateActivityDefinition("BuildFactory", true, 4);
        foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            await _workerRepository.CreateWorker(c.ToString());
    }

}
