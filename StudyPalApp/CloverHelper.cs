using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalApp
{
    public class CloverHelper
    {
        public static CloverState GetDashboardState(List<AssignmentModel> assignments)
        {
            if (assignments == null || assignments.Count == 0)
            {
                return new CloverState
                {
                    Message = "Welcome! Start by adding your first assignment 🌱",
                    Image = "clover.jfif",
                    BackgroundColor = Colors.Purple
                };
            }

            var next = assignments
                .OrderBy(a => DateTime.Parse(a.due_date))
                .First();

            var days = (DateTime.Parse(next.due_date) - DateTime.Today).Days;

            if (days == 0)
            {
                return new CloverState
                {
                    Message = "🚨 Due today! Let’s do this!",
                    Image = "clover.jfif",
                    BackgroundColor = Colors.OrangeRed
                };
            }

            return new CloverState
            {
                Message = "You're on track — keep going!",
                Image = "clover.jfif",
                BackgroundColor = Colors.Green
            };
        }
        public static CloverState GetProgressState(double progress)
        {
            if (progress == 0)
            {
                return new CloverState
                {
                    Message = "Start a study session or take a quiz to boost your progress 🌱",
                    Image = "clover.jfif",
                    BackgroundColor = Colors.Purple
                };
            }

            return new CloverState
            {
                Message = "Awesome progress! Let’s keep increasing productivity 🚀",
                Image = "clover.jfif",
                BackgroundColor = Colors.Green
            };
        }
        public static CloverState GetAssignmentState(List<AssignmentModel> assignments)
        {
            if (assignments == null || assignments.Count == 0)
            {
                return new CloverState
                {
                    Message = "No assignments yet — add one to get started 📚",
                    Image = "clover.jfif",
                    BackgroundColor = Colors.Purple
                };
            }

            var upcoming = assignments
                .Where(a => DateTime.Parse(a.due_date) >= DateTime.Today)
                .OrderBy(a => DateTime.Parse(a.due_date))
                .FirstOrDefault();

            if (upcoming != null)
            {
                var daysLeft = (DateTime.Parse(upcoming.due_date) - DateTime.Today).Days;

                if (daysLeft == 0)
                {
                    return new CloverState
                    {
                        Message = "🚨 Due today! No cramming — start now!",
                        Image = "clover.jfif",
                        BackgroundColor = Colors.Red
                    };
                }

                if (daysLeft <= 2)
                {
                    return new CloverState
                    {
                        Message = "⚠️ Due soon — don’t leave it last minute!",
                        Image = "clover.jfif",
                        BackgroundColor = Colors.Orange
                    };
                }
            }

            return new CloverState
            {
                Message = "You’re on track — keep it up!",
                Image = "clover.jfif",
                BackgroundColor = Colors.Green
            };
        }
        public static CloverState GetQuizState(int score)
        {
            if (score <= 3)
            {
                return new CloverState
                {
                    Message = "Good try! Review and come back stronger 💪",
                    Image = "clover.jfif",
                    BackgroundColor = Colors.Orange
                };
            }

            return new CloverState
            {
                Message = "Great job! You smashed that quiz 🎉",
                Image = "clover.jfif",
                BackgroundColor = Colors.Green
            };
        }
    }
}
