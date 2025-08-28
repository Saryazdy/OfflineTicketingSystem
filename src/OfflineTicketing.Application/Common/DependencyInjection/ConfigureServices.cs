using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OfflineTicketing.Application.Common.Behaviours;
using OfflineTicketing.Application.Common.Mapper;


namespace OfflineTicketing.Application.Common.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 1. AutoMapper
            services.AddAutoMapper(typeof(TicketProfile).Assembly);

            // 2. MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));

            // 3. Pipeline Behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResultBehaviour<,>));


            return services;
        }
    }
}
