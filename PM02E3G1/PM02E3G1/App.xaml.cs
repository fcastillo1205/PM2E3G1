using PM02E3G1.Services;
using PM02E3G1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM02E3G1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.Examen.ListViewPagos());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
