using Microsoft.Extensions.DependencyInjection;
using GymAppADO.Database;
using GymAppADO.Repository;
using GymAppADO.Services;
using GymAppADO.Screens;

namespace GymAppADO
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Configuración ADO.NET
            var dbConfig = new DatabaseConfig();
            dbConfig.InitializeDatabase(); // Autocrea la tabla si no existe
            services.AddSingleton(dbConfig);

            // Inyección de dependencias
            services.AddScoped<IMiembroRepository, MiembroRepository>();
            services.AddScoped<IMiembroService, MiembroService>();
            services.AddScoped<MainScreen>();

            var serviceProvider = services.BuildServiceProvider();

            // Iniciar aplicación
            var mainScreen = serviceProvider.GetRequiredService<MainScreen>();
            mainScreen.ShowMenu();
        }
    }
}
