using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace StudyPalApp
{
    public class QuizModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [JsonPropertyName("option1")]
        public string Option1 { get; set; } = string.Empty;

        [JsonPropertyName("option2")]
        public string Option2 { get; set; } = string.Empty;

        [JsonPropertyName("option3")]
        public string Option3 { get; set; } = string.Empty;

        [JsonPropertyName("option4")]
        public string Option4 { get; set; } = string.Empty;

        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
