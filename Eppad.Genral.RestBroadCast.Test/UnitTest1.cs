using Eppad.General.RestBroadCast;
using RestSharp;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Eppad.Genral.RestBroadCast.Test
{

    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        [Fact]
        public void Test1()
        {
            var client = new RestBroadCastClient(new List<string>() { "http://ti.test.eppad.com", "http://ti.iran.ins.eppad.com" });

            var action = "/api/TransportOrder/TrackOrder/1343848";
            var restRequest = new RestRequest(action, Method.GET);

            client.GetAsync(restRequest, (res, t) => { 
                output.WriteLine($"{res} -> {t}"); });
        }
    }
}
