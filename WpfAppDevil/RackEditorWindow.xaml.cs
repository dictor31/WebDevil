using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using WebDevil.Model;

namespace WpfAppDevil
{
    public partial class RackEditorWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Devil> Devils { get; set; }
        public Rack Rack {  get; set; }

        HttpClient client = new();

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

        public event PropertyChangedEventHandler? PropertyChanged;

        public RackEditorWindow(ObservableCollection<Devil> devils)
        {
            InitializeComponent();

            Rack = new();
            Devils = devils;
            client.BaseAddress = new Uri("https://localhost:7141/api/");

            DataContext = this;
        }
        public RackEditorWindow(Rack rack, ObservableCollection<Devil> devils)
        {
            InitializeComponent();

            Rack = rack;
            Devils = devils;
            client.BaseAddress = new Uri("https://localhost:7141/api/");

            DataContext = this;
        }
        private async void Save(object sender, RoutedEventArgs e)
        {
            if (Rack.Id == 0)
            {
                string json = JsonSerializer.Serialize(Rack);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync("Rack/AddRack", content);
                if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Result = await responce.Content.ReadAsStringAsync();
                }
                else
                {
                    Result = "Мебель создана";
                }
            }
            else
            {
                string json = JsonSerializer.Serialize(Rack);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PutAsync("Rack/PutRack", content);
                if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Result = await responce.Content.ReadAsStringAsync();
                }
                else
                {
                    Result = "Мебель изменена";
                }
            }
            MessageBox.Show($"{Result}");
            Close();
        }

    }
}
