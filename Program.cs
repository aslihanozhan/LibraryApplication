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
            // Dependency Injection servis saðlayýcýsý oluþtur
            var serviceProvider = ConfigureServices();

            // Veritabanýný otomatik migrate etmek için (isteðe baðlý)
            using (var context = serviceProvider.GetRequiredService<LibraryContext>())
            {
                context.Database.Migrate();
            }

            // Windows Forms uygulamasýný baþlat
            ApplicationConfiguration.Initialize();
            Application.Run(serviceProvider.GetRequiredService<Form1>());
        }

        private static ServiceProvider ConfigureServices()
        {
            // DI için servis koleksiyonu oluþtur
            var services = new ServiceCollection();

            // DbContext'i ekle
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer("Server=DESKTOP-O0CU96P\\LIBRARY;Database=KutuphaneDB;Trusted_Connection=True;TrustServerCertificate=True;"));

            // Formlarý ekle
            services.AddTransient<Form1>();

            // Servis saðlayýcýsýný oluþtur
            return services.BuildServiceProvider();
        }
    }
}
