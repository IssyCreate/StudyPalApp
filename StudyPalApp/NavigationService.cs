using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalApp
{
    public class NavigationService
    {
        public static async Task GoToAsync(Page page)
        {
            var nav = Application.Current?.MainPage?.Navigation;

            if (nav == null)
                return;

            await nav.PushAsync(page);
        }

        public static async Task GoBackAsync()
        {
            var nav = Application.Current?.MainPage?.Navigation;

            if (nav == null)
                return;

            await nav.PopAsync();
        }

        public static async Task GoHomeAsync()
        {
            var nav = Application.Current?.MainPage?.Navigation;

            if (nav == null)
                return;

            await nav.PopToRootAsync();
        }

        public static void ResetTo(Page page)
        {
            Application.Current!.MainPage = new NavigationPage(page);
        }
    }
}
