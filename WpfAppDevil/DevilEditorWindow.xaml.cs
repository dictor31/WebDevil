using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebDevil.Model;

namespace WpfAppDevil
{
    /// <summary>
    /// Логика взаимодействия для DevilEditorWindow.xaml
    /// </summary>
    public partial class DevilEditorWindow : Window, INotifyPropertyChanged
    {
        public Devil Devil { get; set; }

        HttpClient client = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
            }
        }

        public DevilEditorWindow()
        {
            InitializeComponent();

            Devil = new();
            client.BaseAddress = new Uri("https://localhost:7141/api/");


            DataContext = this;
        }
        public DevilEditorWindow(Devil devil)
        {
            InitializeComponent();

            Devil = devil;
            client.BaseAddress = new Uri("https://localhost:7141/api/");


            DataContext = this;
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            if (Devil.Id == 0)
            {
                Devil.Racks = [];
                string json = JsonSerializer.Serialize(Devil);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync("Devil/AddDevil", content);
                if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Result = await responce.Content.ReadAsStringAsync();
                }
                else
                {
                    Result = "Дьявол создан";
                }
            }
            else
            {
                Devil.Racks = [];
                string json = JsonSerializer.Serialize(Devil);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PutAsync("Devil/PutDevil", content);
                if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Result = await responce.Content.ReadAsStringAsync();
                }
                else
                {
                    Result = "Дьявол изменен";
                }
            }
            MessageBox.Show($"{Result}");
            Close();
        }
    }
}
