using SwaggerAPITest.DataBase;
using SwaggerAPITest.Services;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureService(this IServiceCollection services)
    {
        services.AddSingleton<AtmService>();
        services.AddSingleton<IAtmEventBroker, AtmEventBroker>();
        services.AddSingleton<IBankService, BankService>();
        services.AddSingleton<IAtmService, AtmEventService>(sp
                => new AtmEventService(
                        sp.GetRequiredService<AtmService>(),
                        sp.GetRequiredService<IAtmEventBroker>()));
        services.AddSingleton<BankDbContext>();
        services.AddSingleton<IAtmLinkGenerator, AtmLinkGenerator>();
    }
}
