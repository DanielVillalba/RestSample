using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ReST
{
    public class RestClient
    {
        public string Serialize()
        {
            Country[] countries = new[]
            {
                new Country {Name = "Mexico" },
                new Country { Name = "EUA" }
            };

            string json = JsonConvert.SerializeObject(countries);
            Debug.WriteLine(json);

            return json;
        }

        public void Deserialize()
        {
            var json = Serialize();

            var parsedJson = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);

            foreach (Country item in parsedJson)
                Debug.WriteLine(item.Name);
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await GetAsJson();
        }


        protected string BaseUrl { get; set; } = "http://restcountries.eu/rest/v1";

        protected async Task<IEnumerable<Country>> GetAsJson()
        {
            IEnumerable<Country> result = Enumerable.Empty<Country>();


            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(BaseUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(json);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        result = await Task.Run(() => { return JsonConvert.DeserializeObject<IEnumerable<Country>>(json); })
                        .ConfigureAwait(false);
                    }
                }
            }
            return result;

        }
    }
}
