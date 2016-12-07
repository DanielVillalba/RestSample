using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ReST
{
    public partial class App : Application
    {
        private MainPage _mainPage;
        private ClientsPage _clientsPage;
        private OffLineSynClientsPage _offLineClientsPage;
        public App()
        {
            _mainPage = new ReST.MainPage();
            _clientsPage = new ClientsPage();
            _offLineClientsPage = new OffLineSynClientsPage();

            InitializeComponent();

            MainPage = _offLineClientsPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
            _clientsPage.Load();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
