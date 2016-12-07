using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReST
{
    [DataTable("Clientes")]
    public class Client
    {
        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty("Nombre")]
        public string Name { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
