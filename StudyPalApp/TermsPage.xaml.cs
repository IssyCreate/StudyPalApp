namespace StudyPalApp;

public partial class TermsPage : ContentPage
{
    public TermsPage()
    {
        InitializeComponent();

        continueBtn.IsEnabled = false;

        agreeCheck.CheckedChanged += (s, e) =>
        {
            continueBtn.IsEnabled = e.Value;
        };
    }

    private async void OnContinueClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginSignupPage());
    }
}