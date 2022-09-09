using SwaggerAPITest.Services;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection services)
    {
        services.AddSingleton<IAtmService, AtmService>();
    }
}
