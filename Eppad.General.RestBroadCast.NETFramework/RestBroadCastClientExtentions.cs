using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Eppad.General.RestBroadCast.NETFramework
{
    public static class RestBroadCastClientExtentions
    {

        /// <summary>
        /// BroadCasts the Requests and for each reasponse callback result
        /// </summary>
        /// <param name="broadCastclient">BroadCast Client</param>
        /// <param name="request">Rest Sharp Request</param>
        /// <param name="callback">Call back method wich calls for every response</param>
        /// <returns>list of Handlers</returns>
        public static List<RestRequestAsyncHandle> GetAsync(this IRestBroadCastClient broadCastclient, IRestRequest request,
           Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            var relults = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var r = client.GetAsync(request, (res, t) => {
                    callback(res, t);
                });
                relults.Add(r);
            }
            return relults;
        }

        public static List<RestRequestAsyncHandle> GetGatherAsync<T>(this IRestBroadCastClient broadCastclient, IRestRequest request,
           Action<List<BroadCastResponse<T>>, RestRequestAsyncHandle> callback) where T : new()
        {
            var results = new List<BroadCastResponse<T>>();
            var handlers = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var handle = client.GetAsync<T>(request, (res, t) => {
                    results.Add(new BroadCastResponse<T>{ Url = client.BaseUrl, Data = res.Data });
                    if (results.Count == broadCastclient.Clients.Count) { callback(results, t); }
                });
                handlers.Add(handle);
            }
            return handlers;
        }

        public static List<RestRequestAsyncHandle> GetGatherFirst<T>(this IRestBroadCastClient broadCastclient, IRestRequest request,
          Action<List<BroadCastResponse<T>>, RestRequestAsyncHandle> callback) where T : new()
        {
            var results = new List<BroadCastResponse<T>>();
            var handlers = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var handle = client.GetAsync<T>(request, (res, t) => {
                    results.Add(new BroadCastResponse<T> { Url = client.BaseUrl, Data = res.Data });
                    if (res.StatusCode == HttpStatusCode.OK) { 
                        handlers.ForEach(x => x.Abort());
                        callback(results, t); 
                        }
                });
                handlers.Add(handle);
            }
            return handlers;
        }


        /// <summary>
        /// BroadCast Request and return tasks of results to handle by caller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broadCastclient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<Task<T>> GetAsync<T>(this IRestBroadCastClient broadCastclient, IRestRequest request) where T : new()
        {
            var results = new List<Task<T>>();
            foreach (var client in broadCastclient.Clients)
            {
                var r = client.GetAsync<T>(request);
                results.Add(r);
            }
            return results;
        }




        /// <summary>
        /// BroadCasts the Requests and for each reasponse callback result
        /// </summary>
        /// <param name="broadCastclient">BroadCast Client</param>
        /// <param name="request">Rest Sharp Request</param>
        /// <param name="callback">Call back method wich calls for every response</param>
        /// <returns>list of Handlers</returns>
        public static List<RestRequestAsyncHandle> PostAsync(this IRestBroadCastClient broadCastclient, IRestRequest request,
           Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            var relults = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var r = client.PostAsync(request, (res, t) => {
                    callback(res, t);
                });
                relults.Add(r);
            }
            return relults;
        }

        public static List<RestRequestAsyncHandle> PostGatherAsync<T>(this IRestBroadCastClient broadCastclient, IRestRequest request,
           Action<List<BroadCastResponse<T>>, RestRequestAsyncHandle> callback) where T : new()
        {
            var results = new List<BroadCastResponse<T>>();
            var handlers = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var handle = client.PostAsync<T>(request, (res, t) => {
                    results.Add(new BroadCastResponse<T> { Url = client.BaseUrl, Data = res.Data });
                    if (results.Count == broadCastclient.Clients.Count) { callback(results, t); }
                });
                handlers.Add(handle);
            }
            return handlers;
        }

        public static List<RestRequestAsyncHandle> PostGatherFirst<T>(this IRestBroadCastClient broadCastclient, IRestRequest request,
          Action<List<BroadCastResponse<T>>, RestRequestAsyncHandle> callback) where T : new()
        {
            var results = new List<BroadCastResponse<T>>();
            var handlers = new List<RestRequestAsyncHandle>();
            foreach (var client in broadCastclient.Clients)
            {
                var handle = client.PostAsync<T>(request, (res, t) =>
                {
                    results.Add(new BroadCastResponse<T> { Url = client.BaseUrl, Data = res.Data, Status = res.StatusCode });
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        handlers.ForEach(x => x.Abort());
                        callback(results, t);
                    }
                    else if (results.Count == broadCastclient.Clients.Count)
                    {
                        callback(results, t);
                    }
                });
                handlers.Add(handle);
            }
            return handlers;
        }


        /// <summary>
        /// BroadCast Request and return tasks of results to handle by caller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broadCastclient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<Task<T>> PostAsync<T>(this IRestBroadCastClient broadCastclient, IRestRequest request) where T : new()
        {
            var results = new List<Task<T>>();
            foreach (var client in broadCastclient.Clients)
            {
                var r = client.PostAsync<T>(request);
                results.Add(r);
            }
            return results;
        }
    }
   
}
