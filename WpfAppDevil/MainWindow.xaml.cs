using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebDevil.Model;

namespace WpfAppDevil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Devil> Devils { get; set; } = new();
        public ObservableCollection<Rack> Racks { get; set; } = new();
        public ObservableCollection<Disposal> Disposals { get; set; } = new();
        public Devil SelectedDevil { get; set; }
        public Rack SelectedRack { get; set; }

        HttpClient client = new();
        private string result;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Result 
        {
            get => result; 
            set 
            {
                result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
            }

        }

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("https://localhost:7141/api/");
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

            DataContext = this;
        }

        private async void Timer_Tick(object? sender, EventArgs e)
        {
            Devils.Clear();
            var responceDevil = await client.GetAsync($"Devil/GetDevils");
            var responceBodyDevil = await responceDevil.Content.ReadAsStringAsync();
            ObservableCollection<Devil> devils = JsonConvert.DeserializeObject<ObservableCollection<Devil>>(responceBodyDevil);
            if (responceDevil.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Result = await responceDevil.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                foreach (var devil in devils)
                {
                    Devils.Add(devil);
                }
            }

            //------------------------------------------------------------------------

            Disposals.Clear();
            var responceDisposal = await client.GetAsync($"Disposal/GetDisposals");
            var responceBodyDisposal = await responceDisposal.Content.ReadAsStringAsync();
            ObservableCollection<Disposal> disposals = JsonConvert.DeserializeObject<ObservableCollection<Disposal>>(responceBodyDisposal);
            if (responceDisposal.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Result = await responceDisposal.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                foreach (var disposal in disposals)
                {
                    Disposals.Add(disposal);
                }
            }

            //------------------------------------------------------------------------

            Racks.Clear();
            var responceRack = await client.GetAsync($"Rack/GetRacks");
            var responceBodyRack = await responceRack.Content.ReadAsStringAsync();
            ObservableCollection<Rack> racks = JsonConvert.DeserializeObject<ObservableCollection<Rack>>(responceBodyRack);
            if (responceRack.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Result = await responceRack.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                foreach (var rack in racks)
                {
                    Racks.Add(rack);
                }
            }
        }

        private void Click_EditorNew(object sender, RoutedEventArgs e)
        {
            DevilEditorWindow window = new();
            window.ShowDialog();
        }

        private void Click_Editor(object sender, RoutedEventArgs e)
        {
            DevilEditorWindow window = new(SelectedDevil);
            window.ShowDialog();
        }

        private void Click_REditorNew(object sender, RoutedEventArgs e)
        {
            RackEditorWindow window = new(Devils);
            window.ShowDialog();
        }

        private void Click_REditor(object sender, RoutedEventArgs e)
        {
            RackEditorWindow window = new(SelectedRack, Devils);
            window.ShowDialog();
        }
    }
}