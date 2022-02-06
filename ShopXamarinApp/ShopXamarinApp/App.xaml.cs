using ShopXamarinApp.Classes;
using ShopXamarinApp.Models;
using ShopXamarinApp.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopXamarinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AuthPage());
            MainPage.Title = (MainPage as NavigationPage).Title;

            Global.Api = new ApiWorker();
            Global.Cart = new List<Product>();
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
