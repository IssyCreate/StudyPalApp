namespace StudyPalApp;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.Json;
using StudyPalApp;

public partial class AssignmentsPage : ContentPage
{
    readonly ApiService api = new ();
    public AssignmentsPage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<ProgressUpdateMessage>(
       this,
       async (r, m) =>
       {
           await LoadAssignments();
       });

    }
    
    private async void OnAddAssignmentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text) ||
            string.IsNullOrWhiteSpace(subjectEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Fill all required fields",
                "OK"
            );

            return;
        }

        string dueDate =
            dueDatePicker.Date.ToString("yyyy-MM-dd");

        string dueTime =
            dueTimePicker.Time.ToString(@"hh\:mm");

        bool success =
            await api.AddAssignment(
                titleEntry.Text,
                subjectEntry.Text,
                instructionsEditor.Text ?? "",
                dueDate,
                dueTime,
                UserSession.UserId
            );

        if (success)
        {
            await DisplayAlert(
                "Success",
                "Assignment added",
                "OK"
            );

            titleEntry.Text = "";
            subjectEntry.Text = "";
            instructionsEditor.Text = "";

            await LoadAssignments();
        }
        else
        {
            await DisplayAlert(
                "Error",
                "Failed to add assignment",
                "OK"
            );
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await LoadAssignments();
    }

    // ================= LOAD =================
    private async Task LoadAssignments()
    {
        try
        {
            var assignments =
                await api.GetAssignments(UserSession.UserId);

            assignmentList.ItemsSource = assignments;

            var state =
                CloverHelper.GetAssignmentState(assignments);

            CloverMessage.Text = state.Message;

            cloverImage.Source = state.Image;

            bannerFrame.BackgroundColor = state.BackgroundColor;
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.Message,
                "OK"
            );
        }
    }
}