using Newtonsoft.Json;
using ShopXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ShopXamarinApp.Classes
{
    class ApiWorker
    {
        private static WebClient webClient;
        private string API_URL = "http://shop.u1590589.plsk.regruhosting.ru/api";

        public ApiWorker()
        {
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Set("Content-Type", "application/json");
        }

        public User GetUser(string login, string password)
        {
            string jsonResponse = webClient.DownloadString(API_URL + "/users");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonResponse);

            User user = users.FirstOrDefault(_user => _user.Login == login && _user.Password == password);

            return user;
        }
    }
}
