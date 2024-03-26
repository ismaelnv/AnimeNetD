using AnimeNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnimeNet.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Generos : ContentPage
    {

        private readonly String _opcionSeleccionada;
        private readonly CatalogoDeAnimes _catalogoDeAnimes;

        public Generos( String opcionSeleccionada)
        {
            InitializeComponent();
            _catalogoDeAnimes = new CatalogoDeAnimes();
            _opcionSeleccionada = opcionSeleccionada;
            getAnimesByGenre();
            GetGenero();
        }
    
        public async void getAnimesByGenre()
        {
            string genero = _opcionSeleccionada;

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri($"http://192.168.1.8:5092/api/genres/animes/{genero}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
             
                string cont = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Anime>>(cont);

                _catalogoDeAnimes.getImages(resultado);
                _catalogoDeAnimes.getGenres(resultado);
                CollectionAnimes.ItemsSource = resultado;
            }

        }

        public  void GetGenero()
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