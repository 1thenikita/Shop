using ShopXamarinApp.Classes;
using ShopXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopXamarinApp.Pages
{
    /// <summary>
    /// Класс страницы продуктов.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        /// <summary>
        /// Инициализация страницы.
        /// </summary>
        public ProductsPage()
        {
            InitializeComponent();

            LviewProducts.ItemsSource = Global.Api.GetProducts();
        }

        /// <summary>
        /// Обработчик добавления продукта в корзину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LviewProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (LviewProducts.SelectedItem == null)
                    return;

                Product product = LviewProducts.SelectedItem as Product;
                if (!product.IsActual)
                {
                    await DisplayAlert("Ошибка", "Продукт не актуален и не доступен к заказу.", "OK");
                    return;
                }

                if (Global.Cart.FirstOrDefault(_p => _p.ID == product.ID) != null)
                {
                    await DisplayAlert("Ошибка", "Продукт уже в корзине.", "OK");
                    return;
                }

                Global.Cart.Add(product);
                bool goToCart = await DisplayAlert("Продукт добавлен", "Нажмите Да если хотите перейти в корзину.", "Да", "Нет");

                if (goToCart)
                {
                    await Navigation.PushAsync(new CartPage());
                }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}
