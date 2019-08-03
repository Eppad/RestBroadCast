using RestSharp;
using System.Collections.Generic;

namespace Eppad.General.RestBroadCast.NETFramework
{
    public interface IRestBroadCastClient
    {
        List<RestClient> Clients { get; set; }
    }
   
}
