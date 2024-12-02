using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using System.Configuration;

namespace LibraryApplication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Dependency Injection servis sa�lay�c�s� olu�tur
            var serviceProvider = ConfigureServices();

            // Veritaban�n� otomatik migrate etmek i�in (iste�e ba�l�)
            using (var context = serviceProvider.GetRequiredService<LibraryContext>())
            {
                context.Database.Migrate();
            }

            // Windows Forms uygulamas�n� ba�lat
            ApplicationConfiguration.Initialize();
            Application.Run(serviceProvider.GetRequiredService<Form1>());
        }

        private static ServiceProvider ConfigureServices()
        {
            // DI i�in servis koleksiyonu olu�tur
            var services = new ServiceCollection();

            // DbContext'i ekle
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer("Server=DESKTOP-O0CU96P\\LIBRARY;Database=KutuphaneDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            // Formlar� ekle
            services.AddTransient<Form1>();

            // Servis sa�lay�c�s�n� olu�tur
            return services.BuildServiceProvider();
        }
    }
}
