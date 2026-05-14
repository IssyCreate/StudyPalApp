using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalApp
{
    public class AssignmentModel
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public string subject { get; set; } = "";
        public string instructions { get; set; } = "";
        public string due_date { get; set; } = "";
        public string due_time { get; set; } = "";
        public string user_id { get; set; } = "";
    }
}

