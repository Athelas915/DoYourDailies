using DoYourDailies.Data.Implementations;
using DoYourDailies.Data.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace DoYourDailies.Data.Extensions
{
    public static class Extensions
    {
        private const string GetCurrentTimeQuery = "SELECT CURRENT_TIMESTAMP";

        public static WebApplication EnsureDatabaseUpdated(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DoYourDailiesContext>();
                
                context?.Database.Migrate();
            }

            return app;
        }

        public static DateTime GetCurrentTimeFromDb(this DoYourDailiesContext context)
        {
            var con = context.Database.GetDbConnection();
            var cmd = con.CreateCommand();
            cmd.CommandText = GetCurrentTimeQuery;
            con.Open();
            var dateString = cmd.ExecuteScalar();
            var dateTime = DateTime.TryParse(dateString?.ToString(), out var value) ? value : DateTime.Now;
            con.Close();
            return dateTime;
        }

        public static async Task<DateTime> GetCurrentTimeFromDbAsync(this DoYourDailiesContext context)
        {
            var con = context.Database.GetDbConnection();
            var cmd = con.CreateCommand();
            cmd.CommandText = GetCurrentTimeQuery;
            con.Open();
            var dateString = await cmd.ExecuteScalarAsync();
            var dateTime = DateTime.TryParse(dateString?.ToString(), out var value) ? value : DateTime.Now;
            con.Close();
            return dateTime;
        }
    }
}
