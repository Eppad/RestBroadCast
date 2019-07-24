using System;
using System.Net;

namespace Eppad.General.RestBroadCast
{
    public class BroadCastResponse<T>
    {
        public Uri Url { get; set; }
        public T Data { get; set; }
        public HttpStatusCode Status { get; set; }
    }
   
}
