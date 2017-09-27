using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPX.Views;
using Xamarin.Forms;

namespace MPX
{
    public class App : Application
    {
        public App()
        {
            if (Device.Idiom == TargetIdiom.Desktop)
                MainPage = new NavigationPage(new DesktopView());

            else if (Device.Idiom == TargetIdiom.Phone)
                MainPage = new WebView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
