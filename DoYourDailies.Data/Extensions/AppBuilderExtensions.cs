using DoYourDailies.Data.Implementations;
using DoYourDailies.Data.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace DoYourDailies.Data.Extensions
{
    public static class AppBuilderrExtensions
    {
        public static WebApplicationBuilder AddDataLayer(this WebApplicationBuilder builder)
        {
            builder.AddDatabaseProviders();
            builder.Services.AddScoped<IServerClock, ServerClock>();

            return builder;
        }

        private static WebApplicationBuilder AddDatabaseProviders(this WebApplicationBuilder builder)
        {
            var databaseConfig = builder.Configuration?
                .GetSection(DatabaseOptions.Section)?
                .Get<List<DatabaseOptions>>()?
                .Where(o => o.Use)?
                .ToDictionary(o => o.Name, o => o.ConnectionString);

            if (databaseConfig == null)
                return builder;

            if (databaseConfig.TryGetValue(DatabaseOptions.SQLite, out var sqlite))
            {
                builder.AddSqlite(sqlite);
            }

            // Add different providers here if necessary in the future

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            return builder;
        }

        private static WebApplicationBuilder AddSqlite(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddEntityFrameworkSqlite().AddDbContext<DoYourDailiesContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(
                connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly($"{nameof(DoYourDailies)}.{nameof(Data)}")
                );
            });
            return builder;
        }
    }
}
