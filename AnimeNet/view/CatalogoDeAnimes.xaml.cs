using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnimeNet.Model;
using AnimeNet.ErrorHandler;
using AnimeNet.Service;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogoDeAnimes : ContentPage
    {
        public Color CustomBlackColor { get; set; }
        private readonly ApiService _apiService;

        public CatalogoDeAnimes()
        {
            _apiService = new ApiService();
            InitializeComponent();
            CargarAnimes();
            LoadGenres();
        }

       private async void CargarAnimes()
       {

            try
            {

                string response = await _apiService.GetDataFromApi("http://192.168.1.8:5092/api/animes");

                var resultado = JsonConvert.DeserializeObject<List<Anime>>(response);

                //Si la lista de animes no está vacía
                if (resultado != null)
                {
                    getImages(resultado);
                    getGenres(resultado);
                    CollectionAnimes.ItemsSource = resultado;
                }
                else
                {

                    StackLayout vistaError = ApiErrorHandler.errorBack("Error al cargar datos");
                    Content = vistaError;
                    Console.WriteLine("error al deserealizar anime response");
                }
            }
            catch (Exception ex)
            {

                StackLayout vistaError = ApiErrorHandler.errorBack("Error al cargar datos");
                Content = vistaError;
                Console.WriteLine(ex.Message);
            }  
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

        private async void LoadGenres()
        {

            try
            {

                string response = await _apiService.GetDataFromApi("http://192.168.1.8:5092/api/genres");

                var resultado = JsonConvert.DeserializeObject<List<Genre>>(response);

                List<string> generos = new List<string>();

                foreach (Genre genre in resultado)
                {

                    generos.Add(genre.name);
                }

                picker.ItemsSource = generos;

            }
            catch (Exception ex)
            {

                StackLayout vistaError = ApiErrorHandler.errorBack("Error al cargar datos");
                Content = vistaError;
                Console.WriteLine(ex.Message);
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