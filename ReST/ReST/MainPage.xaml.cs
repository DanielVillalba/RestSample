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
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Country> Countries { get; set; }

        private RestClient _client;

        public Command RefreshCommand { get; set; }


        public MainPage()
        {
            RefreshCommand = new Command(() => Load());
            Countries = new ObservableCollection<Country>();
            _client = new RestClient();

            InitializeComponent();

        }

        public async void Load()
        {
            
            var result = await _client.GetCountries();
            Countries.Clear();

            foreach(Country item in result)
            {
                Countries.Add(item);
            }

            IsBusy = false;

        }
    }
}
