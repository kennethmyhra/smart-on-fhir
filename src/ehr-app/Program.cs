using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using Microsoft.Extensions.Configuration;

namespace EHRApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            InitializeConfiguration();
            InitializeCef();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIParent());
        }

        static void InitializeConfiguration(string profile = null)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("AppSettings.json");
            if(profile != null)
                builder.AddJsonFile($"{profile}.json");

            IConfigurationRoot configurationRoot = builder.Build();

            Globals.ApplicationSettings = new ApplicationSettings();
            configurationRoot.GetSection("ApplicationSettings").Bind(Globals.ApplicationSettings);

            Globals.SmartAppSettings = new SmartAppSettings();
            configurationRoot.GetSection("SmartAppSettings").Bind(Globals.SmartAppSettings);
        }

        static void InitializeCef()
        {
            Cef.EnableHighDPISupport();
            var settings = new CefSettings
            {
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"CefSharp\Cachce")
            };
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
        }
    }
}
