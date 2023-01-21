using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Validation;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<KonnichiwaDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase"),
        builder => builder.MigrationsAssembly(typeof(KonnichiwaDbContext).Assembly.FullName)));


        services.AddScoped<IKonnichiwaDbContext, KonnichiwaDbContext>();
        services.AddScoped<IValidator<User>, UserValidator>();

        return services;
    }
}