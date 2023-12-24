using AnimeNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDeAnime : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private Anime _anime;

        public DetalleDeAnime(Anime anime)
        {
            InitializeComponent();

            _anime = anime; // Guarda el item en una variable de la clase.
            getChapters();

            // Asegúrate de establecer el contexto de enlace a este item si vas a utilizar enlaces en tu XAML.
            BindingContext = _anime;
        }

        public async void getChapters()
        {
            int idAnime = _anime.Id; 
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://192.168.1.6:5092/api/animes/{idAnime}/capitulos");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string cont = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Anime>(cont);

                foreach (Chapter chapter in resultado.Chapters) 
                {

                    foreach( var image in chapter.Images) 
                    {
                        if (image.imageType == 2)
                        {

                            chapter.imageCover = image.name;
                        }
                    }
                    
                }

                CollectionChapters.ItemsSource = resultado.Chapters;

               // CollectionAnimes.ItemsSource = resultado;
            }
        }

        private async void btnRegreso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CatalogoDeAnimes());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
