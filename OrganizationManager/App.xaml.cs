using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace OrganizationManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient HttpClient = new HttpClient();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            HttpClient.BaseAddress = new Uri("https://localhost:7135");

        }
    }
}
