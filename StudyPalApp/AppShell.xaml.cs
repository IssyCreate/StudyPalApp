namespace StudyPalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginSignupPage));
            Routing.RegisterRoute("terms", typeof(TermsPage));
            Routing.RegisterRoute("dashboard", typeof(DashboardPage));
            Routing.RegisterRoute("timetable", typeof(TimetablePage));
        }
    }
}
