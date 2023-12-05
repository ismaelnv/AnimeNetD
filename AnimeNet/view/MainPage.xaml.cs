using AnimeNet.view;
using System;
using Xamarin.Forms;

namespace AnimeNet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CatalogoDeAnimes());
        }
    }
}
