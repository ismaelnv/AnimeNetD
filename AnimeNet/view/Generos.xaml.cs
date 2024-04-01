using AnimeNet.ErrorHandler;
using AnimeNet.Model;
using AnimeNet.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Generos : ContentPage
    {

        private readonly String _opcionSeleccionada;
        private readonly CatalogoDeAnimes _catalogoDeAnimes;
        private readonly ApiService _apiService;


        public Generos(String opcionSeleccionada)
        {

            InitializeComponent();
            _apiService = new ApiService();
            _catalogoDeAnimes = new CatalogoDeAnimes();
            _opcionSeleccionada = opcionSeleccionada;
            GetAnimesByGenre();
            GetGenero();
        }
    
        public async void GetAnimesByGenre()
        {

            try
            {

                string genero = _opcionSeleccionada;
                String response = await _apiService.GetDataFromApi($"http://192.168.1.8:5092/api/genres/animes/{genero}");
                var resultado = JsonConvert.DeserializeObject<List<Anime>>(response);

                _catalogoDeAnimes.getImages(resultado);
                _catalogoDeAnimes.getGenres(resultado);
                CollectionAnimes.ItemsSource = resultado;

            }catch (Exception ex)
            {

                StackLayout vistaError = ApiErrorHandler.errorBack("Error al cargar datos");
                Content = vistaError;
                Console.WriteLine(ex.Message);
            }
        }

        public void GetGenero()
        {

           string generos = _opcionSeleccionada;

            Dictionary<string, (string descripcion, string imagen)> generosDic = new Dictionary<string, (string, string)>
            {
                { "Accion", ("¡Para que no extrañes tus explosiones del día!", "accion.png") },
                { "Aventura", ("¡Disfruta con los héroes y de cómo persiguen sus sueños!", "aventura.png") },
                { "Comedia", ("¡Ríe a carcajadas con chistes clásicos y bromas modernas sin sentido.", "comedia.png") },
                { "Deportes", ("¡No puedes vivir sin tu pelota(o sin hacer algún tipo de ejercicio físico)!Pues entra aquí.", "deporte.png") },
                { "Fantasia", ("¡La magia y los monstruos no son nada contra los aventureros!", "fantasia.png") },
                { "Romance", ("¿Quieres que tu corazón se acelere y creer en el amor? Satisface aquí tu romanticismo.", "romance.png") },
                { "Sobre natural", ("Fantasmas, espíritus, posesiones, ¡y todo tipo de criaturas!", "sobrenatural.png") },
                { "Mecha",("¡Prepárate para vivir la emoción de los robots gigantes con nuestra selección de contenido mecha!","mecha.png")  },
                {"Anime",("¡Disfruta de los mejores animes!","anime.png") }
            };

            // Verificar si el género seleccionado está en el diccionario
            if (generosDic.ContainsKey(generos))
            {
                // Obtener la información del género seleccionado del diccionario
                var generoInfo = generosDic[generos];

                // Asignar la información a las etiquetas y la imagen
                lblGenero.Text = _opcionSeleccionada;
                lblDescripcion.Text = generoInfo.descripcion;
                icoGenero.Source = generoInfo.imagen;
            }

        }

        private async void Frame_Tapped(object sender, EventArgs e)
        {

            var frame = (Frame)sender;
            var item = (Anime)frame.BindingContext; // Asegúrate de reemplazar 'TuModeloDeDatos' con el tipo de tu modelo.
            await Navigation.PushAsync(new DetalleDeAnime(item)); // Reemplaza detalleAnime con el nombre real de tu página de detalles
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CatalogoDeAnimes());
        }

    }
}