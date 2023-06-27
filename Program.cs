using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSC.Services;

namespace WSC
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDownloadSite, DownloadSite>();
            services.AddScoped<HttpClient>();
            services.AddScoped<HtmlAgilityPack.HtmlDocument>();
            ServiceProvider = services.BuildServiceProvider();
        }

        public static T? GetService<T>() where T : class
        {
            return (T?)ServiceProvider.GetService(typeof(T));

        }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Run(new FormMain());
        }
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message =
                String.Format(
                    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "Please contact support.",
                    ((Exception)e.ExceptionObject).Message);
            MessageBox.Show(message, @"Unexpected error");
        }

        /// <summary>
        /// Global exceptions in User Interface handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message =
                String.Format(
                    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "Please contact support.",
                    e.Exception.Message);
            MessageBox.Show(message, @"Unexpected error");
        }
    }
}
