namespace StudyPalApp
{
    public partial class MainPage : ContentPage
    {
    

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initial positions
            LogoImage.TranslationY = 60;
            LogoImage.Scale = 0.5;
            LogoImage.Opacity = 0;

            AppName.Opacity = 0;
            Slogan.Opacity = 0;

            // 🔥 LOGO: Slide + Scale + Fade in
            await Task.WhenAll(
                LogoImage.TranslateTo(0, 0, 800, Easing.CubicOut),
                LogoImage.ScaleTo(1.1, 800, Easing.CubicOut),
                LogoImage.FadeTo(1, 800)
            );

            // 🔥 Bounce finish
            await LogoImage.ScaleTo(1, 200, Easing.BounceOut);

            await Task.Delay(150);

            // App Name appears
            await AppName.FadeTo(1, 500, Easing.CubicOut);

            await Task.Delay(150);

            // Slogan appears
            await Slogan.FadeTo(1, 500, Easing.CubicOut);

            // Let user see it
            await Task.Delay(2000);

            // Fade everything out smoothly
            await Task.WhenAll(
                LogoImage.FadeTo(0, 600, Easing.CubicIn),
                AppName.FadeTo(0, 600, Easing.CubicIn),
                Slogan.FadeTo(0, 600, Easing.CubicIn)
            );

            // Navigate to Terms page
            await Shell.Current.GoToAsync("terms");
        }

    }
}
