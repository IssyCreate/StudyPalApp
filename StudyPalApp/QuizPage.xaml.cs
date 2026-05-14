namespace StudyPalApp;


public partial class QuizPage : ContentPage
{
   readonly ApiService api = new ApiService();

    List<QuizModel> questions = new();

    int currentQuestion = 0;
    int score = 0;

    // TIMER
    int timeLeft = 300; 
    bool isQuizActive = false;
    CancellationTokenSource? timerToken;
    public QuizPage()
	{
		InitializeComponent();
        StartQuiz();
    }

    // ================= START QUIZ =================
    private async void StartQuiz()
    {
        questions = await api.GetQuiz();

        if (questions == null || questions.Count == 0)
        {
            await DisplayAlert("Error", "No quiz available", "OK");
            return;
        }

        currentQuestion = 0;
        score = 0;
        isQuizActive = true;

        StartTimer();
        ShowQuestion();
    }
    // ================= TIMER =================
    private void StartTimer()
    {
        timerToken = new CancellationTokenSource();

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (!isQuizActive || timerToken.IsCancellationRequested)
                return false;

            timeLeft--;

            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;

            timerLabel.Text = $"{minutes:00}:{seconds:00}";

            if (timeLeft <= 0)
            {
                FinishQuiz();
                return false;
            }


            return true;
        });
    }
   
    // ================= SHOW QUESTION =================
    private void ShowQuestion()
    {
        if (currentQuestion >= questions.Count)
        {
            FinishQuiz();
            return;
        }

        var q = questions[currentQuestion];

        questionLabel.Text = q.Question;

        option1Btn.Text = q.Option1;
        option2Btn.Text = q.Option2;
        option3Btn.Text = q.Option3;
        option4Btn.Text = q.Option4;

        // counter update
        counterLabel.Text = $"{currentQuestion + 1}/{questions.Count}";

        // progress bar
        quizProgress.Progress =
            (double)(currentQuestion + 1) / questions.Count;
    }

    // ================= ANSWER CLICK =================
    private void OnAnswerClicked(object sender, EventArgs e)
    {
        if (!isQuizActive) return;

        Button btn = (Button)sender;

        string selected = btn.Text;

        if (selected == questions[currentQuestion].CorrectAnswer)
        {
            score++;
        }

        scoreLabel.Text = $"Score: {score}";

        currentQuestion++;

        ShowQuestion();
    }
    private async void OnHintClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Hint",
            "Think carefully about the topic before answering.",
            "OK"
        );
    }
    // ================= FINISH QUIZ =================
    private async void FinishQuiz()
    {
        if (!isQuizActive) return;

        isQuizActive = false;

        await api.SaveQuizResult(
            UserSession.UserId,
            score
        );
        await api.SaveStudySession(
    UserSession.UserId,
    "Quiz",
    score * 10
);

        await DisplayAlert(
    "Quiz Complete",
    $"You scored {score}/{questions.Count}",
    "Awesome"
);

        
        var state = CloverHelper.GetQuizState(score);

        CloverMessage.Text = state.Message;
        cloverImage.Source = state.Image;
        bannerFrame.BackgroundColor = state.BackgroundColor;

        await Navigation.PopAsync();
    }
}