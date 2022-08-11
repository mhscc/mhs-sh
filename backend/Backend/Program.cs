using Ng.Services;
using System.Text.Json.Serialization;

namespace Backend
{
    public class Program
    {
        public static UserAgentService? UAService { get; private set; }

        public static void Main(string[] args)
        {
            var settings = new UserAgentSettings
            {
                CacheSizeLimit = 5 * 1000,
                CacheSlidingExpiration = TimeSpan.FromDays(3),
                UaStringSizeLimit = 256
            };

            UAService = new(settings);

            ThreadStart[] workers = {
                ExpirationChecker.Checker,
                AccessReporter.Worker
            };

            foreach (var i in workers)
                new Thread(i).Start();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers()
                .AddJsonOptions(i =>
                {
                    var slopes = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull;
                    i.JsonSerializerOptions.DefaultIgnoreCondition = slopes;
                });

            var app = builder.Build();

            app.UseCors(
                i => i.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}