using Xamarin.Forms;

namespace AnimeNet.ErrorHandler
{
    public class ApiErrorHandler
    {

        public static StackLayout errorBack(string menssage) 
        {
            Xamarin.Forms.Image imageError = new Xamarin.Forms.Image
            {

                Source = "error.png",
                WidthRequest = 300, // Ancho deseado para la imagen
                HeightRequest = 300, // Alto deseado para la imagen
                Margin = new Thickness(0, 100, 0, 0)

            };

            Label menssageError = new Label
            {

                Text = menssage,
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
            error.Children.Add(menssageError);

            return error;
        }
    }
}
