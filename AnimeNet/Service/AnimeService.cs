using AnimeNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;

namespace AnimeNet.Service
{
    class AnimeService
    {

        public async void getAnimes()
        {

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.1.6:5092/api/animes");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
        } 

    }
}
