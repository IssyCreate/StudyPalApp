using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
namespace StudyPalApp;

public partial class ProgressPage : ContentPage
{
    readonly ApiService api = new();
    public ProgressPage()
	{
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<ProgressUpdateMessage>(
         this,
         (r, m) =>
         {
             LoadProgress();
         });
    }

    private async void LoadProgress()
    {
        try
        {
            var json =
                await api.GetStudyStats(UserSession.UserId);

          

            // EMPTY CHECK
            if (string.IsNullOrWhiteSpace(json))
            {
                dailyGoalLabel.Text = "0 / 120 mins";
                sessionsLabel.Text = "Sessions Completed: 0";
                streakLabel.Text = "Current Streak: 0 days";
                totalMinutesLabel.Text = "Total Study Minutes: 0";

                CloverMessage.Text =
                    "Complete quizzes and study sessions to earn progress!";

                return;
            }

            using JsonDocument doc =
                JsonDocument.Parse(json);

            int totalMinutes = 0;

            // ================= GET TOTAL =================
            if (doc.RootElement.TryGetProperty(
                "total",
                out var totalElement))
            {
                // DEBUG TYPE
                Console.WriteLine(
                    "TOTAL VALUE KIND: " +
                    totalElement.ValueKind
                );

                if (totalElement.ValueKind ==
                    JsonValueKind.Number)
                {
                    totalMinutes =
                        totalElement.GetInt32();
                }
                else if (totalElement.ValueKind ==
                         JsonValueKind.String)
                {
                    int.TryParse(
                        totalElement.GetString(),
                        out totalMinutes
                    );
                }
            }

            // ================= DAILY GOAL =================
            int goal = 120;

            double progress =
                (double)totalMinutes / goal;

            if (progress > 1)
                progress = 1;

            // ================= UPDATE UI =================
            dailyGoalLabel.Text =
                $"{totalMinutes} / {goal} mins";

            totalMinutesLabel.Text =
                $"Total Study Minutes: {totalMinutes}";

            sessionsLabel.Text =
                $"Sessions Completed: {(totalMinutes > 0 ? 1 : 0)}";

            streakLabel.Text =
                $"Current Streak: {(totalMinutes > 0 ? 1 : 0)} days";

            dailyProgressBar.Progress =
                progress;

            // ================= CLOVER MESSAGE =================
            if (totalMinutes == 0)
            {
                CloverMessage.Text =
                    "Let's begin your first study session";
            }
            else if (totalMinutes < 60)
            {
                CloverMessage.Text =
                    "Good progress so far — keep going";
            }
            else
            {
                CloverMessage.Text =
                    "Amazing consistency today!";
            }
        }
        catch (Exception ex)
        {
            

            await DisplayAlert(
                "Progress Error",
                ex.Message,
                "OK"
            );

            dailyGoalLabel.Text =
                "0 / 120 mins";

            sessionsLabel.Text =
                "Sessions Completed: 0";

            streakLabel.Text =
                "Current Streak: 0 days";

            totalMinutesLabel.Text =
                "Total Study Minutes: 0";

            CloverMessage.Text =
                "Progress data unavailable right now.";
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadProgress();
    }

}