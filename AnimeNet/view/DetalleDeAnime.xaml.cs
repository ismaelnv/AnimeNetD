using AnimeNet.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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

            // Asegúrate de establecer el contexto de enlace a este item si vas a utilizar enlaces en tu XAML.
            BindingContext = _anime;
            Items = new ObservableCollection<string>
            {
                _anime.name,
                _anime.image,
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
