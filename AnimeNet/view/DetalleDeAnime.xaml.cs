using AnimeNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnimeNet.ErrorHandler;
using AnimeNet.Service;
using System.Threading.Tasks;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleDeAnime : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        private Anime _anime;
        private readonly ApiService _apiService;

        public DetalleDeAnime(Anime anime)
        {

            InitializeComponent();
            _apiService = new ApiService();
            _anime = anime; // Guarda el item en una variable de la clase.
            GetChapters();

            // Asegúrate de establecer el contexto de enlace a este item si vas a utilizar enlaces en tu XAML.
            BindingContext = _anime;
        }

        public async void GetChapters()
        {

            try
            {

                int idAnime = _anime.Id;
                string response = await _apiService.GetDataFromApi($"http://192.168.1.4:5092/api/animes/{idAnime}/capitulos");
                var resultado = JsonConvert.DeserializeObject<Anime>(response);

                foreach (Chapter chapter in resultado.Chapters)
                {

                    foreach (var image in chapter.Images)
                    {

                        if (image.imageType == 2)
                        {

                            chapter.imageCover = image.name;
                        }
                    }

                }

                CollectionChapters.ItemsSource = resultado.Chapters;

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
