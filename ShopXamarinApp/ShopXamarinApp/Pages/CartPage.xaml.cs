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
    /// Класс страницы корзины.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private float totalPrice = 0;

        /// <summary>
        /// Инициализация страницы.
        /// </summary>
        public CartPage()
        {
            InitializeComponent();
            LoadCartAndTotalPrice();
        }

        /// <summary>
        /// Обработчик выбора продукта для удаления.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LviewCart_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (LviewCart.SelectedItem == null)
                    return;

                Product product = LviewCart.SelectedItem as Product;

                bool deleteProduct = await DisplayAlert("Подтверждение", "Подтвердите удаление продукта из корзины.", "Да", "Нет");
                if (!deleteProduct) return;
                Global.Cart.Remove(product);

                LoadCartAndTotalPrice();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");

            }
        }

        /// <summary>
        /// Обработчик обновления данных.
        /// </summary>
        private async void LoadCartAndTotalPrice()
        {
            try
            {
                List<Product> products = Global.Cart;

                totalPrice = 0;
                foreach (Product product in products)
                {
                    totalPrice += product.Price;
                }

                lblTotalPrice.Text = "Общая цена: " + totalPrice.ToString();

                LviewCart.ItemsSource = null;
                LviewCart.ItemsSource = products;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Обработчик покупки продуктов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBuy_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (totalPrice > Global.MyUser.Balance)
                {
                    await DisplayAlert("Ошибка", "У Вас недостаточно денег.\nНе хватает: " + (totalPrice - Global.MyUser.Balance).ToString(), "OK");
                    return;
                }

                Global.Api.AddPurchases();
                Global.Cart.Clear();
                await DisplayAlert("Успешно", "Покупка выполнена.", "OK");
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}
