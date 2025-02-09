using Microsoft.Extensions.DependencyInjection;
using Tycoon.Factory.Core.Interfaces;

namespace Tycoon.Factory.Infrastructure
{
    public static class ServiceManagerExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            services.AddSingleton<IWorkerRepository, WorkerRepository>()
                .AddSingleton<IAssignmentRepository, AssignmentRepository>()
                .AddSingleton<IActivityRepository, ActivityRepository>();
    }
}