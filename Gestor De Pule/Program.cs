using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using var context = new DataBase();
            //context.Database.Migrate();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            context.Database.Migrate();
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}