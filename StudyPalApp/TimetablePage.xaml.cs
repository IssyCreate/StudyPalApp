using System.Linq;
using Plugin.LocalNotification;
using CommunityToolkit.Mvvm.Messaging;
namespace StudyPalApp;


public partial class TimetablePage : ContentPage
{
    readonly ApiService api = new();
    readonly CancellationTokenSource? timerToken;
    readonly int remainingSeconds;
    readonly bool isRunning;
    private bool sessionRunning = false; 
    string selectedDay = "Mon";
    readonly List<TimetableModel> allData = [];
    int totalSeconds;

    public TimetablePage()
    {
        InitializeComponent();
        
       
    }
    
    // ================= LOAD TIMETABLE =================
    private async Task LoadTimetable()
    {
        try
        {
            var data = await api.GetTimetable(UserSession.UserId);

            allData.Clear();

            if (data != null)
                allData.AddRange(data);

            LoadSchedule();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadTimetable();
    }

    // ================= DAY FILTER =================
    private void OnDayClicked(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;

        selectedDay = btn.Text;
        LoadSchedule();
    }

    private void LoadSchedule()
    {
        scheduleList.ItemsSource =
            allData.Where(x => x.Day == selectedDay).ToList();
    }

    // ================= START SESSION =================
    private async void OnStartSessionClicked(object sender, EventArgs e)
    {
        if (sessionRunning)
        {
            await DisplayAlert("Wait", "A session is already running.", "OK");
            return;
        }

        if (sender is not Button btn)
            return;

        if (btn.BindingContext is not TimetableModel session)
            return;

        sessionRunning = true;
        int duration = session.DurationMinutes;

        totalSeconds = session.DurationMinutes * 60;
        timerLabel.Text = $"{session.DurationMinutes:00}:00";

        int firstBreak = totalSeconds * 2 / 3;
        int secondBreak = totalSeconds / 3;

        bool firstBreakShown = false;
        bool secondBreakShown = false;

        timerLabel.Text = $"{duration:00}:00";

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (!sessionRunning)
                return false;

            totalSeconds--;

            if (totalSeconds < 0)
                totalSeconds = 0;

            int mins = totalSeconds / 60;
            int secs = totalSeconds % 60;

            timerLabel.Text = $"{mins:00}:{secs:00}";

            // FIRST BREAK
            if (!firstBreakShown &&
                totalSeconds <= firstBreak &&
                totalSeconds > secondBreak)
            {
                firstBreakShown = true;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert(
                        "Clover Reminder",
                        $"Take a quick stretch break during {session.Subject}.",
                        "OK"
                    );
                });
            }

            // SECOND BREAK
            if (!secondBreakShown &&
                totalSeconds <= secondBreak &&
                totalSeconds > 0)
            {
                secondBreakShown = true;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert(
                        "Clover Reminder",
                        "Rest your eyes and hydrate.",
                        "OK"
                    );
                });
            }

            // SESSION COMPLETE
            if (totalSeconds <= 0)
            {
                sessionRunning = false;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    timerLabel.Text = "00:00";

                    int finalDuration = session.DurationMinutes;

                    await api.SaveStudySession(
                        UserSession.UserId,
                        session.Subject,
                        finalDuration
                    );
                    WeakReferenceMessenger.Default.Send(
                              new ProgressUpdateMessage()
                       );

                    await DisplayAlert(
                        "Session Complete",
                        $"{session.Subject} session saved successfully!",
                        "OK"
                    );
                });

                return false;
            }

            return true;
        });
    }

    // ================= ADD TIMETABLE ITEM =================
    private async void OnAddClicked(object sender, EventArgs e)
    {
        string subject = await DisplayPromptAsync("New Subject", "Enter subject name");
        if (string.IsNullOrWhiteSpace(subject)) return;

        string time = await DisplayPromptAsync("Start Time", "Enter time (example: 2:00 PM)");
        if (string.IsNullOrWhiteSpace(time)) return;

        string durationText = await DisplayPromptAsync("Study Duration", "Enter duration in minutes");

        if (!int.TryParse(durationText, out int durationMinutes))
        {
            await DisplayAlert("Invalid Input", "Please enter a valid number.", "OK");
            return;
        }

        allData.Add(new TimetableModel
        {
            Subject = subject,
            Time = time,
            DurationMinutes = durationMinutes,
            Day = selectedDay
        });

        await api.SaveTimetable(
            UserSession.UserId,
            subject,
            time,
            durationMinutes,
            selectedDay
        );

        await LoadTimetable();
    }

   
    private async Task ShowCloverBreak(string subject)
    {
        await DisplayAlert(
            "Clover Break Reminder",
            $"Great job studying {subject}! Take a break.",
            "OK"
        );
    }

    private static async Task SendBreakNotification(string subject)
    {
        var request = new NotificationRequest
        {
            NotificationId = 100,
            Title = "Clover Reminder",
            Description = $"You've been studying {subject}. Take a short break!",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(1)
            }
        };

        await LocalNotificationCenter.Current.Show(request);
    }
}