namespace StudyPalApp;

public partial class LoginSignupPage : ContentPage
{
   readonly ApiService api = new ();

    public LoginSignupPage()
    {
        InitializeComponent();
        ShowLogin(); // default screen
    }

    // ---------- UI TOGGLES ----------
    private void ShowLogin()
    {
        loginForm.IsVisible = true;
        signupForm.IsVisible = false;
    }

    private void ShowSignup()
    {
        loginForm.IsVisible = false;
        signupForm.IsVisible = true;
    }

    private void OnLoginTabClicked(object sender, EventArgs e)
    {
        ShowLogin();
    }

    private void OnSignupTabClicked(object sender, EventArgs e)
    {
        ShowSignup();
    }

    // ---------- LOGIN ----------
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        bool success = await api.Login(loginEmail.Text, loginPassword.Text);

        if (success)
        {
            await Shell.Current.GoToAsync("//dashboard");
        }
        else
        {
            await DisplayAlert("Error", "Invalid login", "OK");
        }
    }

    // ---------- SIGNUP ----------
    private async void OnSignupClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(signupName.Text) ||
            string.IsNullOrWhiteSpace(signupEmail.Text) ||
            string.IsNullOrWhiteSpace(signupPassword.Text) ||
            string.IsNullOrWhiteSpace(signupAge.Text))
        {
            await DisplayAlert("Error", "Fill in all fields", "OK");
            return;
        }

        bool success = await api.Signup(
            signupName.Text,
            signupEmail.Text,
            signupPassword.Text,
            signupAge.Text
        );

        if (success)
        {
            await DisplayAlert("Success", "Account created!", "OK");
            ShowLogin();
        }
        else
        {
            await DisplayAlert("Error", "Signup failed", "OK");
        }
    }

    // ---------- GOOGLE (placeholder) ----------
    private async void OnGoogleClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "Google login coming later", "OK");
    }

    
}