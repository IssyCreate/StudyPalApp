using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyPalApp
{


    public class FirebaseService
    {
        private readonly HttpClient _http = new HttpClient();

        private const string projectId = "YOUR_PROJECT_ID";

        public async Task SaveNoteBackup(string userId, string title, string content)
        {
            var url = $"https://firestore.googleapis.com/v1/projects/{projectId}/databases/(default)/documents/notes_backup";

            var data = new
            {
                fields = new
                {
                    userId = new { stringValue = userId },
                    title = new { stringValue = title },
                    content = new { stringValue = content },
                    timestamp = new { timestampValue = DateTime.UtcNow.ToString("o") }
                }
            };

            var json = JsonSerializer.Serialize(data);

            var contentData = new StringContent(json, Encoding.UTF8, "application/json");

            await _http.PostAsync(url, contentData);
        }
    }
}
