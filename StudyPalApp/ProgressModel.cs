using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalApp
{
    public class ProgressModel
    {
        public int TotalStudyMinutes { get; set; }
        public int TotalQuizScore { get; set; }

        public int XP { get; set; }
        public int Level { get; set; }
        public double ProgressToNextLevel { get; set; }
    }
}
