using System.Text;
using System.Text.Json;
namespace StudyPalApp;

public class ApiService
{
    readonly HttpClient client = new HttpClient();
    //used to test windows em
    readonly string baseUrl = "http://192.168.0.104/studypalapi/api/";
    //used to test mobile em
    //readonly string baseUrl = "http://10.0.2.2/studypalapi/api/";

    // ---------- LOGIN ----------
    public async Task<bool> Login(string email, string password)
    {
        var data = new Dictionary<string, string>
        {
            { "email", email },
            { "password", password }
        };

        var content = new FormUrlEncodedContent(data);

        var response = await client.PostAsync(baseUrl + "login.php", content);
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<LoginResponse>(json);

        if (result?.status == "success")
        {
            UserSession.UserId = result.id;
            UserSession.Name = result.name;
            return true;
        }

        return false;
    }

    // ---------- SIGNUP ----------
    public async Task<bool> Signup(string name, string email, string password, string age)
    {
        var data = new Dictionary<string, string>
        {
            { "name", name },
            { "email", email },
            { "password", password },
            { "age", age }
        };

        var content = new FormUrlEncodedContent(data);

        var response = await client.PostAsync(baseUrl + "signup.php", content);
        var result = await response.Content.ReadAsStringAsync();

        return result.Contains("success");
    }


    // ---------- ADD ASSIGNMENT ----------
    public async Task<bool> AddAssignment(
    string title,
    string subject,
    string instructions,
    string dueDate,
    string dueTime,
    int userId)
    {
        var data = new Dictionary<string, string>
    {
        { "title", title },
        { "subject", subject },
        { "instructions", instructions },
        { "due_date", dueDate },
        { "due_time", dueTime },
        { "user_id", userId.ToString() }
    };

        var content = new FormUrlEncodedContent(data);

        var response = await client.PostAsync(baseUrl + "addAssignments.php", content);
        var result = await response.Content.ReadAsStringAsync();

        return result.Contains("success");
    }
    public async Task<List<AssignmentModel>> GetAssignments(int userId)
    {
        var json = await client.GetStringAsync(
            baseUrl + "getassignments.php?user_id=" + userId
        );

        Console.WriteLine(json);

        return JsonSerializer.Deserialize<List<AssignmentModel>>(json)
               ?? new List<AssignmentModel>();
    }

    // ---------- Save StudySessions ---------
    public async Task SaveStudySession(int userId, string subject, int duration)
    {
        var data = new
        {
            user_id = userId,
            subject = subject,
            duration_minutes = duration
        };

        var json = JsonSerializer.Serialize(data);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        await client.PostAsync(baseUrl + "savesession.php", content);
    }
    public async Task<string> GetStudyStats(int userId)
    {
        return await client.GetStringAsync(
            baseUrl + "getstudystat.php?user_id=" + userId
        );
    }
    // ================= ADD NOTE =================
    public async Task AddNote(int userId, string subject, string content)
    {
        var data = new
        {
            user_id = userId,
            subject = subject,
            content = content
        };

        var json = JsonSerializer.Serialize(data);

        var httpContent = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        await client.PostAsync(
            baseUrl + "addNotes.php",
            httpContent
        );
    }

    // ================= GET NOTES =================
    public async Task<List<NotesModel>> GetNotes(int userId)
    {
        var json = await client.GetStringAsync(
            baseUrl + "getNotes.php?user_id=" + userId
        );

        Console.WriteLine("NOTES RAW JSON: " + json);

        return JsonSerializer.Deserialize<List<NotesModel>>(json)
               ?? new List<NotesModel>();
    }

    // ================= DELETE NOTE =================
    public async Task DeleteNote(int id)
    {
        await client.GetAsync(
            baseUrl + "deleteNotes.php?id=" + id
        );
    }
    // ================= GET QUIZ =================
    public async Task<List<QuizModel>> GetQuiz()
    {
        var json = await client.GetStringAsync(
            baseUrl + "getQuiz.php"
        );

        return JsonSerializer.Deserialize<List<QuizModel>>(json)
               ?? new List<QuizModel>();
    }

    // ================= SAVE QUIZ RESULT =================
    public async Task SaveQuizResult(int userId, int score)
    {
        var data = new
        {
            user_id = userId,
            score = score
        };

        var json = JsonSerializer.Serialize(data);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        await client.PostAsync(
            baseUrl + "saveQuizResults.php",
            content
        );
    }
    public async Task SaveTimetable(
    int userId,
    string subject,
    string startTime,
    int durationMinutes,
    string day)
    {
        var data = new
        {
            user_id = userId,
            subject = subject,
            start_time = startTime,
            duration_minutes = durationMinutes,
            day = day
        };

        var json =
            JsonSerializer.Serialize(data);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

        await client.PostAsync(
             baseUrl + "save_timetable.php",
            content
        );
    }
    
    public async Task<List<QuizResultModel>> GetQuizResults(int userId)
    {
        var json = await client.GetStringAsync(
            baseUrl + "getQuizResults.php?user_id=" + userId
        );

        return JsonSerializer.Deserialize<List<QuizResultModel>>(json)
               ?? new List<QuizResultModel>();
    }
    public async Task<List<QuizModel>> GetQuizResults()
    {
        var json = await client.GetStringAsync(
            baseUrl + "getQuiz.php"
        );

        return JsonSerializer.Deserialize<List<QuizModel>>(json)
               ?? new List<QuizModel>();
    }
    public async Task<List<TimetableModel>> GetTimetable(int userId)
    {
        try
        {
            var json = await client.GetStringAsync(
                baseUrl + $"get_timetable.php?user_id={userId}"
            );

            if (string.IsNullOrWhiteSpace(json))
                return new List<TimetableModel>();

            return JsonSerializer.Deserialize<List<TimetableModel>>(json)
                   ?? new List<TimetableModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("GetTimetable Error: " + ex.Message);
            return new List<TimetableModel>();
        }
    }
}