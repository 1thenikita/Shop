using ShopXamarinApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopXamarinApp.Pages
{
    /// <summary>
    /// Класс страницы аутентифицации.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        /// <summary>
        /// Инициализация страницы.
        /// </summary>
        public AuthPage()
        {
            InitializeComponent();

            object login = "";

            if (App.Current.Properties.TryGetValue("Login", out login))
            {
                entryLogin.Text = (string)login;
                entryPassword.Text = (string)App.Current.Properties["Password"];
                cbSaveUserData.IsChecked = true;
            }
        }

        /// <summary>
        /// Обработчик кнопки начала аутентификации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(entryLogin.Text) || String.IsNullOrWhiteSpace(entryPassword.Text))
                {
                    DisplayAlert("Ошибка", "Заполните логин и/или пароль!", "OK");
                    return;
                }

                Global.MyUser = Global.Api.GetUserAtLoginAndPassword(entryLogin.Text, entryPassword.Text);

                if (Global.MyUser == null)
                {
                   await DisplayAlert("Ошибка", "Пользователь не найден!", "OK");
                    return;
                }

                if (cbSaveUserData.IsChecked)
                {
                    App.Current.Properties["Login"] = entryLogin.Text;
                    App.Current.Properties["Password"] = entryPassword.Text;
                }
                else
                {
                    entryLogin.Text = "";
                    entryPassword.Text = "";
                    App.Current.Properties["Login"] = null;
                    App.Current.Properties["Password"] = null;
                }

                await Navigation.PushAsync(new MenuPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
            
        }

        /// <summary>
        /// Обработчик отображения пароля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbVisiblePass_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            entryPassword.IsPassword = !cbVisiblePass.IsChecked;
        }

        private void cbSaveUserData_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}