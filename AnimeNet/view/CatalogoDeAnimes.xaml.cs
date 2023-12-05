using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnimeNet.Model;
using AnimeNet.Service;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoDeAnimes : ContentPage
    {


        public CatalogoDeAnimes()
        {

            InitializeComponent();
            cargarAnimes();
        }

       private async void cargarAnimes()
       {

           var request = new HttpRequestMessage();
           request.RequestUri = new Uri("http://192.168.1.6:5092/api/animes");
           request.Method = HttpMethod.Get;
           request.Headers.Add("Accept", "application/json");
           var client = new HttpClient();
          HttpResponseMessage response = await client.SendAsync(request);

           if (response.StatusCode == HttpStatusCode.OK)
           {
                string cont = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Anime>>(cont);

             // ListAnime.ItemsSource = resultado;

                CollectionAnimes.ItemsSource = resultado;
           }
       }

        private async void Frame_Tapped(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var item = (Anime)frame.BindingContext; // Asegúrate de reemplazar 'TuModeloDeDatos' con el tipo de tu modelo.
            await Navigation.PushAsync(new DetalleDeAnime(item)); // Reemplaza detalleAnime con el nombre real de tu página de detalles
        }
    }
}