using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Honlsoft.TimeLog;

public static class UseCaseExtensions {

    public static void AddDomain(this IServiceCollection serviceCollection) {

        // Add all use cases from this assembly.
        serviceCollection.AddMediatR(typeof(UseCaseExtensions).Assembly);
    }

}