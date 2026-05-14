using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace StudyPalApp
{
    public class TimetableModel
    {
        [JsonPropertyName("Subject")]
        public string Subject { get; set; } = "";

        [JsonPropertyName("Time")]
        public string Time { get; set; } = "";

        [JsonPropertyName("DurationMinutes")]
        public int DurationMinutes { get; set; }

        [JsonPropertyName("Day")]
        public string Day { get; set; } = "";
    }
}
