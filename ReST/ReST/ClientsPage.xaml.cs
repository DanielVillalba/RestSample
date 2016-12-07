using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ReST
{
    public partial class ClientsPage : ContentPage
    {
        private AzureClient _client;
        public ObservableCollection<Client> Items { get; set; }
        public Command RefreshCommand { get; set; }


        public ClientsPage()
        {
            _client = new AzureClient();
            Items = new ObservableCollection<Client>();
            RefreshCommand = new Command(() => Load());
            InitializeComponent();
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
