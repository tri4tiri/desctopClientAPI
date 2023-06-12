using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Task16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static HttpClient? httpClient;

        public MainWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient();

            var response = httpClient.GetAsync("https://localhost:7065/WeatherForecast").Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            resultText.Content = responseContent.Replace("},{", "},\n");
           

        }

        private void getInfo() // код взят из кнопки GET
        {
            var response = httpClient.GetAsync("https://localhost:7065/WeatherForecast");
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            resultText.Content = responseContent.Replace("},{", "},\n");
        }



        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            getInfo(); 

        }

        private void postButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string date = dateTextBox.Text;
            string degree = degreeTextBox.Text;
            string location = locationTextBox.Text;

            string newRecord = "{ \"id\": " + id +
                "{ \"date\": " + date + "\"" +
                ", \"degree\": " + degree +
                ", \"location\": \"" + location + "\"}";

            var stringContent = new StringContent(newRecord, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("https://localhost:7065/WeatherForecast", stringContent).Result;

            MessageBox.Show(response.StatusCode.ToString());


            getInfo(); // задание 1
        }

        private void putButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string date = dateTextBox.Text;
            string degree = degreeTextBox.Text;
            string location = locationTextBox.Text;

            string newRecord = "{ \"id\": " + id +
                "{ \"date\": " + date + "\"" +
                ", \"degree\": " + degree +
                ", \"location\": \"" + location + "\"}";

            var stringContent = new StringContent(newRecord, Encoding.UTF8, "application/json");

            var response = httpClient.PutAsync("https://localhost:7065/WeatherForecast", stringContent).Result;

            MessageBox.Show(response.StatusCode.ToString());

            getInfo(); // задание 1
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;

            var response = httpClient.DeleteAsync($"https://localhost:7065/WeatherForecast?id={id}").Result;

            MessageBox.Show(response.StatusCode.ToString());

            getInfo(); // задание 1
        }
    }
}