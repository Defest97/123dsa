using DemoClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrganizationManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // thread 1
        //    var r = App.HttpClient.GetAsync("api/organizations"); // r - task2
        //    r.Wait();
        //    var responce = r.Result; 
        //    if (responce.IsSuccessStatusCode)
        //    {
        //        var r1 = responce.Content.ReadFromJsonAsync<QueryResult<Organization>>();
        //        r1.Wait();
        //        dg.ItemsSource = r1.Result?.Items;
        //    }
        //}

        private async Task UpdateGrid()
        {
            var responce = await App.HttpClient.GetAsync("api/organizations"); // r - task2
            if (responce.IsSuccessStatusCode)
            {
                var result = await responce.Content.ReadFromJsonAsync<QueryResult<Organization>>();
                dg.ItemsSource = result?.Items;
            }

        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateGrid();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var organization = new Organization
            {
                Name = "Organization",
                FullName = "Full name organization"
            };
            var responce = await App.HttpClient.PostAsJsonAsync("api/organizations", organization);
            await UpdateGrid();
        }
    }
}
