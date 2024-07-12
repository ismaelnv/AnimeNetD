using AnimeNet.Model;
using AnimeNet.view;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnimeNet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            NavigateToMainPage();
        }

        private async void NavigateToMainPage()
        {
            // Esperar 3 segundos antes de navegar a la página principal
            await Task.Delay(2000);
            await Navigation.PushAsync(new CatalogoDeAnimes());
        }
    }
}
