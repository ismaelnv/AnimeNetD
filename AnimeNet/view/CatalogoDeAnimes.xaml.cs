using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnimeNet.Model;
using System.ComponentModel;
using Xamarin.CommunityToolkit.Converters;
using System.Threading.Tasks;
using System.Linq;


namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoDeAnimes : ContentPage
    {
        public Color CustomBlackColor { get; set; }

        public CatalogoDeAnimes()
        {
 
            InitializeComponent();
            CargarAnimes();
           // loadGenres();
        }

       private async void CargarAnimes()
       {

            try
            {

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://192.168.1.8:5092/api/animes");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                   
                   string cont = await response.Content.ReadAsStringAsync();
                   var resultado = JsonConvert.DeserializeObject<List<Anime>>(cont);

                   //Si la lista de animes no está vacía
                   if(resultado != null)
                   {
                        getImages(resultado);
                        getGenres(resultado);
                        CollectionAnimes.ItemsSource = resultado;
                   }
                }
                else
                {

                    MessageError("Reiniciar la aplicacion porfavor");
                }
            }
            catch(Exception ex) 
            {

               Console.WriteLine(ex.Message);
               MessageError("Problemas al iniciar la aplicacion");
            }
       }

        public void MessageError(string mensaje) 
        {

            Xamarin.Forms.Image imageError = new Xamarin.Forms.Image
            {

                Source = "error.png",
                WidthRequest = 300, // Ancho deseado para la imagen
                HeightRequest = 300, // Alto deseado para la imagen
                Margin = new Thickness(0,100, 0, 0)

            };

            Label mensajeError = new Label
            {

                Text = mensaje,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand, // Centrar el StackLayout horizontalmente
                Margin = new Thickness(0, 150, 0, 0) // Márgenes para ajustar la posición del Label
            };
        
            StackLayout error = new StackLayout
            {

               // Orientation = StackOrientation.Horizontal, // Orientación horizontal para el StackLayout
                HorizontalOptions = LayoutOptions.CenterAndExpand, // Centrar el StackLayout horizontalmente
                VerticalOptions = LayoutOptions.CenterAndExpand // Centrar el StackLayout verticalmente
            };

            error.Children.Add(imageError);
            error.Children.Add(mensajeError);
            
            Content = error;
        }

        public void getImages(List<Anime> resultado) 
        {

            foreach (var anime in resultado)
            {
                foreach (var imagePoster in anime.Images)
                {

                    //anime.imagePoster = imagePoster.name;

                    if (imagePoster.imageCategory == 1)
                    {

                        anime.imagePoster = imagePoster.name;
                    }

                    if (imagePoster.imageCategory == 2)
                    {

                        anime.imageCover = imagePoster.name;
                    }

                }
            }    

        }

        public void getGenres(List<Anime> resultado) 
        {

            foreach(var anime in resultado) 
            {

                if (anime.Genres != null)
                {
                    foreach (Genre genre in anime.Genres)
                    {

                        anime.LblGenres.Append(genre.name).Append(", ");
                    }

                    if (anime.LblGenres.Length > 0)
                    {

                        anime.LblGenres.Length -= 2;
                    }
                }

            }
        }

        private async void loadGenres()
        {

            //probando codigo 

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://192.168.1.8:5092/api/genres");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {

                string cont = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Genre>>(cont);

                List<string> generos = new List<string>();

                foreach (Genre genre in resultado) 
                {

                    generos.Add(genre.name); 
                }

                picker.ItemsSource = generos;
            }
           
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var pickers = (Picker)sender;
            string opcionSeleccionada = (string)picker.SelectedItem;

            if (!string.IsNullOrEmpty(opcionSeleccionada))
            {

                await Navigation.PushAsync( new Generos(opcionSeleccionada));
            }
        }

        private async void Frame_Tapped(object sender, EventArgs e)
        {
    
           var frame = (Frame)sender;
           var item = (Anime)frame.BindingContext; // Asegúrate de reemplazar 'TuModeloDeDatos' con el tipo de tu modelo.
           await Navigation.PushAsync(new DetalleDeAnime(item)); // Reemplaza detalleAnime con el nombre real de tu página de detalles
        }

        private async void btnBusqueda_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Busqueda());
        }
    }
}