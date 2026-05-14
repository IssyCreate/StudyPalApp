namespace StudyPalApp;

public partial class IntroQuizPage : ContentPage
{
	public IntroQuizPage()
	{
		InitializeComponent();
	}
    private async void OnStartQuizClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuizPage());
    }
}