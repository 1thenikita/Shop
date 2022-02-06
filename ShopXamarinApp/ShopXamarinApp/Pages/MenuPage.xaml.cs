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
    /// Класс страницы меню.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        /// <summary>
        /// Инициализация страницы.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = Global.MyUser;
        }

        /// <summary>
        /// Обработчик перехода на страницу покупки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProducts_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductsPage());
        }
    }
}