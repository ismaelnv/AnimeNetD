using AnimeNet.ErrorHandler;
using AnimeNet.Model;
using AnimeNet.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeNet.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Busqueda : ContentPage
	{

        private readonly CatalogoDeAnimes _catalogoDeAnimes;
        private readonly ApiService _apiService;
		public Busqueda ()
		{

            _catalogoDeAnimes = new CatalogoDeAnimes();
            _apiService = new ApiService();
            InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CatalogoDeAnimes());
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {

            
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                string animeName = e.NewTextValue;

                // Verifica si el texto del SearchBar está vacío o nulo
                if (string.IsNullOrWhiteSpace(animeName))
                {
                    // Si está vacío, borra la lista de resultados
                    searchs.ItemsSource = null;
                }
                else
                {
                    // Si hay texto, realiza la llamada a la API y procesa los resultados
                    string response = await _apiService.GetDataFromApi($"http://192.168.1.4:5092/api/animes/name/{animeName}");

                    var resultado = JsonConvert.DeserializeObject<List<Anime>>(response);

                    if(resultado != null)
                    {

                        _catalogoDeAnimes.getImages(resultado);
                        _catalogoDeAnimes.getGenres(resultado);

                        searchs.ItemsSource = resultado;
                    }
                }
            }
            catch (Exception ex)
            {

                StackLayout vistaError = ApiErrorHandler.errorBack("Error al cargar datos");
                Content = vistaError;
                Console.WriteLine(ex.Message);
            }

        }

        private async void Frame_Tapped(object sender, EventArgs e)
        {

            var frame = (Frame)sender;
            frame.BackgroundColor = Color.FromHex("#1A1A1A");
            await Task.Delay(200); // 200 ms, ajusta según tus necesidades
            frame.BackgroundColor = Color.Black;
            var item = (Anime)frame.BindingContext; // Asegúrate de reemplazar 'TuModeloDeDatos' con el tipo de tu modelo.
            await Navigation.PushAsync(new DetalleDeAnime(item)); // Reemplaza detalleAnime con el nombre real de tu página de detalles
        }
    }
}