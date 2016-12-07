using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ReST
{
    public partial class OffLineSynClientsPage : ContentPage
    {
    
        private AzureClient _client;
        public ObservableCollection<Client> Items { get; set; }
        public Command RefreshCommand { get; set; }
        public Command GenerateContactsCommand { get; set;}
        public Command CleanLocalDataCommand { get; set; }


        public OffLineSynClientsPage()
        {
            _client = new AzureClient();
            Items = new ObservableCollection<Client>();
            RefreshCommand = new Command(() => Load());
            GenerateContactsCommand = new Command(() => generateClients());
            CleanLocalDataCommand = new Command(() => cleanLocalData());
            InitializeComponent();
        }

        public async void generateClients()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            string[] names = { "José Luis", "Miguel Ángel", "José Francisco", "Jesús Antonio", "Jorge", "Alberto",
                                "Sofía", "Camila", "Valentina", "Isabella", "Ximena", "Ana"};
            string[] lastNames = { "Hernández", "García", "Martínez", "López", "González", "Méndez", "Castillo", "Corona", "Cruz" };

            Random rdn = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; i++)
            {
                var contact = new Client() { Name = $"{names[rdn.Next(0, 12)]} {lastNames[rdn.Next(0, 8)]}" };
                _client.AddContact(contact);
            }

            IsBusy = false;
        }

        public async Task cleanLocalData()
        {
            await _client.CleanData();
        }

        public async void Load()
        {
            IsBusy = true;

            if (Items.Count() > 0)
                Items.Clear();

            var result = await _client.GetClients();

            foreach (var item in result)
                Items.Add(item);

            IsBusy = false;

        }
    }

}

