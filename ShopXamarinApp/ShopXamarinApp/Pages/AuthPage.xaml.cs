using ShopXamarinApp.Classes;
using ShopXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopXamarinApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            ApiWorker apiWorker = new ApiWorker();
            User user = apiWorker.GetUser(entryLogin.Text, entryPassword.Text);

            if(user == null)
            {
                return;
            }

            Navigation.PushAsync(new MenuPage());
        }
    }
}