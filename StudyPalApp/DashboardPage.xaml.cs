using StudyPalApp;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.Json;
namespace StudyPalApp;

public partial class DashboardPage : ContentPage
{
    readonly ApiService api = new();

    readonly int userId = 1; // TEMP (replace after login)
    public DashboardPage()
	{
		InitializeComponent();
        greetingLabel.Text = $"Hello, {UserSession.Name}!!";
         WeakReferenceMessenger.Default.Register<ProgressUpdateMessage>(
        this,
        async (r, m) =>
        {
            await LoadDashboardProgress();
        });
    }

    // ================= NOTIFICATIONS =================
    private void OnNotificationToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            DisplayAlert(
                "Notifications",
                "Notifications enabled",
                "OK"
            );
        }
        else
        {
            DisplayAlert(
                "Notifications",
                "Notifications disabled",
                "OK"
            );
        }
    }
   /* private async void OnUserSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPagexaml());
    } */

   
    private async void OnNotesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//notes");
    }

    private async void OnQuizClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//quiz");
    }

    // ================= VIEW ALL =================
    private async void OnViewAllScheduleClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//timetable");
    }

    private async void OnViewAllDeadlinesClicked(object sender, EventArgs e)
    {
       await Shell.Current.GoToAsync("//tasks"); 
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadDashboard();

        await LoadDashboardProgress();
    }


    // ================= DASHBOARD DATA =================
    private async Task LoadDashboard()
    {
        try
        {
            // =========================
            // TODAY'S TIMETABLE
            // =========================
            var timetable =
                await api.GetTimetable(UserSession.UserId);

            string today =
                DateTime.Now.DayOfWeek switch
                {
                    DayOfWeek.Monday => "Mon",
                    DayOfWeek.Tuesday => "Tue",
                    DayOfWeek.Wednesday => "Wed",
                    DayOfWeek.Thursday => "Thu",
                    DayOfWeek.Friday => "Fri",
                    DayOfWeek.Saturday => "Sat",
                    _ => "Sun"
                };

            var todaysSchedule =
                timetable
                .Where(t => t.Day == today)
                .ToList();

            todayList.ItemsSource = todaysSchedule;

            // =========================
            // UPCOMING ASSIGNMENTS
            // =========================
            var assignments =
                await api.GetAssignments(UserSession.UserId);

            var upcoming =
                assignments
                .Where(a =>
                    DateTime.Parse(a.due_date) >= DateTime.Today)
                .OrderBy(a =>
                    DateTime.Parse(a.due_date))
                .Take(5)
                .ToList();

            upcomingList.ItemsSource = upcoming;
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

    private async Task LoadDashboardProgress()
    {
        try
        {
            // ================= STUDY STATS =================
            var studyJson =
                await api.GetStudyStats(UserSession.UserId);

            int studyMinutes = 0;

            if (!string.IsNullOrWhiteSpace(studyJson))
            {
                using JsonDocument doc =
                    JsonDocument.Parse(studyJson);

                if (doc.RootElement.TryGetProperty(
                    "total",
                    out var total))
                {
                    studyMinutes = total.GetInt32();
                }
            }

            // ================= QUIZ XP =================
            var quizResults =
                await api.GetQuizResults(UserSession.UserId);

            int quizXP =
                quizResults.Sum(q => q.Score * 10);

            // ================= TOTAL XP =================
            int totalXP =
                studyMinutes + quizXP;

            int level =
                (totalXP / 100) + 1;

            int xpProgress =
                totalXP % 100;

            // ================= UPDATE UI =================
            xpLabel.Text =
                $"XP: {totalXP}";

            levelLabel.Text =
                $"Level {level}";

            xpBar.Progress =
                xpProgress / 100.0;

            streakLabel.Text =
                $"{(studyMinutes > 0 ? 1 : 0)} Day Streak";

            studyProgressLabel.Text =
                $"{studyMinutes} / 120 mins studied today";

            studyProgressBar.Progress =
                Math.Min((double)studyMinutes / 120, 1);
        }
        catch
        {
            xpLabel.Text = "XP: 0";

            levelLabel.Text = "Level 1";

            xpBar.Progress = 0;
        }
    }

   
}