using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Collections.ObjectModel;

namespace ReST
{
    class AzureClient
    {
        private IMobileServiceClient _client;
        //private IMobileServiceTable<Client> _table;
        private IMobileServiceSyncTable<Client> _table; // offline sync approach
        const string dbPath = "clientsDB";

        public AzureClient()
        {
            _client = new MobileServiceClient("http://midemoxamarintiempo.azurewebsites.net");
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Client>();
            _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Client>();
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                await SyncAsync();
            return  await _table.ToEnumerableAsync();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();

                await _table.PullAsync("allClients", _table.CreateQuery());
            }
            catch(MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;

            }
        }

        public async Task CleanData()
        {
            await _table.PurgeAsync("allClients", _table.CreateQuery(), new System.Threading.CancellationToken());
        }

        public async void AddContact(Client contact)
        {
            await _table.InsertAsync(contact);

        }
    }
}
