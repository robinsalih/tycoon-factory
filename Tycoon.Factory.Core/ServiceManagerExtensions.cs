using Microsoft.Extensions.DependencyInjection;
using Tycoon.Factory.Core.Interfaces;
using Tycoon.Factory.Core.Scheduling;

namespace Tycoon.Factory.Core;
public static class ServiceManagerExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services.AddSingleton<IScheduler, Scheduler>()
            .AddSingleton<IScheduleChecker, ScheduleChecker>();
}
