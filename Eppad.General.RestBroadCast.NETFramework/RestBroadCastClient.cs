using RestSharp;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Eppad.General.RestBroadCast.NETFramework
{
    public class RestBroadCastClient : IRestBroadCastClient
    {
        public List<RestClient> Clients { get; set; }

        public RestBroadCastClient(List<string> baseUrls)
        {
            Clients = new List<RestClient>();
            foreach (var url in baseUrls)
                try
                {
                    Clients.Add(new RestClient(url));
                }
                catch (System.Exception)
                {
                    continue;
                }
        }

    }
   
}
