using Microsoft.Extensions.DependencyInjection;
using Tycoon.Factory.Core.Interfaces;

namespace Tycoon.Factory.Core;
public static class ServiceManagerExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services.AddSingleton<IScheduler, Scheduler>();
}
