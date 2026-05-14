namespace StudyPalApp;

public partial class NotesPage : ContentPage
{
   readonly ApiService api = new ();
    private readonly FirebaseService firebase = new();
    List<NotesModel> notes = [];

    public NotesPage()
	{
		InitializeComponent();
        LoadNotes();
    }

    // ================= LOAD NOTES =================
    private async void LoadNotes()
    {
        try
        {
            notes = await api.GetNotes(UserSession.UserId);

            notesCollection.ItemsSource = notes;
        }
        catch
        {
            await DisplayAlert(
                "Error",
                "Unable to load notes",
                "OK"
            );
        }
    }

    // ================= ADD NOTE =================
    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        // FREE LIMIT
        if (notes.Count >= 3)
        {
            await DisplayAlert(
                "Premium Feature",
                "Free users can only create 3 notes. Upgrade to StudyPal Premium for unlimited notes.",
                "OK"
            );

            return;
        }

        // SUBJECT INPUT
        string subject = await DisplayPromptAsync(
            "Subject",
            "Enter subject"
        );

        if (string.IsNullOrWhiteSpace(subject))
            return;

        // NOTE INPUT
        string content = await DisplayPromptAsync(
            "Note",
            "Write your note"
        );

        if (string.IsNullOrWhiteSpace(content))
            return;
        await api.AddNote(
            UserSession.UserId,
            subject,
            content
        );
        try
        {
            await firebase.SaveNoteBackup(
                UserSession.UserId.ToString(),
                subject,
                content
            );
        }
        catch
        {
           
        }
        LoadNotes();

    }

    // ================= DELETE NOTE =================
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        NotesModel note =
            (NotesModel)btn.BindingContext;

        bool confirm = await DisplayAlert(
            "Delete Note",
            "Are you sure you want to delete this note?",
            "Yes",
            "No"
        );

        if (!confirm)
            return;

        await api.DeleteNote(note.Id);

        LoadNotes();
    }

   

    
    
}