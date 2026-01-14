using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using NetSparkleUpdater;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;
using NetSparkleUpdater.SignatureVerifiers;

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

            var sparkle = new SparkleUpdater("https://raw.githubusercontent.com/lopes-obregon/Gestor-De-Pule/refs/heads/master/Gestor%20De%20Pule/appcast.xml",
                new Ed25519Checker(SecurityMode.Strict, "/oSnntxisUdYkJG/Kh8Es6DTZ7gwTMRykGSEKJP8cTE="))
            {
                UIFactory = new NetSparkleUpdater.UI.WPF.UIFactory(),
                RelaunchAfterUpdate= true,


            };
            sparkle.StartLoop(true);

            //context.Database.Migrate();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            context.Database.Migrate();
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}