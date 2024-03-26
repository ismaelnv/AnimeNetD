using AnimeNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeNet.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Busqueda : ContentPage
	{

        private readonly CatalogoDeAnimes _catalogoDeAnimes;
		public Busqueda ()
		{

            _catalogoDeAnimes = new CatalogoDeAnimes();
            InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CatalogoDeAnimes());
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void OnTextChanged(object sender, EventArgs e)
        {

            string animeName = search.Text; 
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://192.168.1.8:5092/api/animes/name/{animeName}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            //List<Anime> results = new List<Anime> {resultado};
            if (response.StatusCode == HttpStatusCode.OK)
            {

                string cont = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Anime>>(cont);

                _catalogoDeAnimes.getImages(resultado);
                _catalogoDeAnimes.getGenres(resultado);
               
                 searchs.ItemsSource = resultado;
            }
            else 
            {

                searchs.ItemsSource = null;
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