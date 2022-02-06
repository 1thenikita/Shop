using Newtonsoft.Json;
using ShopXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ShopXamarinApp.Classes
{
    /// <summary>
    /// Класс работы с API
    /// </summary>
    public class ApiWorker
    {
        private static WebClient webClient;
        private const string ApiUrl = "http://shop.u1590589.plsk.regruhosting.ru/api";

        public ApiWorker()
        {
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers["Content-Type"] = "application/json";
        }

        /// <summary>
        /// Обработчик получения листа пользователей
        /// </summary>
        /// <returns>Лист пользователей</returns>
        public List<User> GetUsers()
        {
            List<User> users = null;
            try 
            {
                string jsonResponse = webClient.DownloadString(ApiUrl + "/Users");
                return JsonConvert.DeserializeObject<List<User>>(jsonResponse);
            }
            catch (Exception ex)
            {
                return users;
            }
            
        }

        /// <summary>
        /// Обработчик получения пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь</returns>
        public User GetUserAtLoginAndPassword(string login, string password)
        {
            User user = null;
            try
            {
                List<User> users = GetUsers();
                return users.FirstOrDefault(_u => _u.Login == login && _u.Password == password);
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        /// <summary>
        /// Обработчик получения листа продуктов
        /// </summary>
        /// <returns>Лист продуктов</returns>
        public List<Product> GetProducts()
        {
            List<Product> products = null;
            try
            {
                string jsonResponse = webClient.DownloadString(ApiUrl + "/Products");
                products = JsonConvert.DeserializeObject<List<Product>>(jsonResponse);
                return products;
            }
            catch (Exception ex)
            {
                return products;
            }
        }

        /// <summary>
        /// Обработчик добавления продуктов к покупке
        /// </summary>
        /// <param name="id">ID покупки</param>
        public void AddProductsInPurchases(int id)
        {
            try 
            {
                List<ProductsInPurchases> products = new List<ProductsInPurchases>();
                foreach (Product productInCart in Global.Cart)
                {
                    products.Add(new ProductsInPurchases()
                    {
                        ProductID = productInCart.ID,
                        PurchaseID = id
                    });
                }

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.UploadString(ApiUrl + "/ProductsInPurchases", JsonConvert.SerializeObject(products));
            }
            catch
            {

            }
        }

        /// <summary>
        /// Обработчик добавления покупки
        /// </summary>
        public void AddPurchases()
        {
            try
            {
                float totalMoney = 0;

                foreach (Product product in Global.Cart)
                {
                    totalMoney += product.Price;
                }

                Purchase purchase = new Purchase()
                {
                    UserID = Global.MyUser.ID,
                    DatePurchase = DateTime.Now,
                    TotalMoney = totalMoney
                };

                string jsonRequest = JsonConvert.SerializeObject(purchase);

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string jsonResponse = webClient.UploadString(ApiUrl + "/Purchases", jsonRequest);

                Purchase purchaseInDB = JsonConvert.DeserializeObject<Purchase>(jsonResponse);

                ChargeMoney(totalMoney);
                AddProductsInPurchases(purchaseInDB.ID);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Обработчик списывания денег с баланса пользователя
        /// </summary>
        /// <param name="money">Деньги</param>
        public void ChargeMoney(float money)
        {
            try
            {
                Global.MyUser.Balance -= money;
                string jsonRequest = JsonConvert.SerializeObject(Global.MyUser);

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.UploadString(ApiUrl + "/Users/" + Global.MyUser.ID, "PUT", jsonRequest);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
