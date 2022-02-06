using ShopXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Classes
{
    /// <summary>
    /// Глобальный класс всех необходимых сущностей и эксемпляров
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public static User MyUser { get; set; }
        /// <summary>
        /// Обработчик API.
        /// </summary>
        public static ApiWorker Api { get; set; }
        /// <summary>
        /// Корзина пользователя.
        /// </summary>
        public static List<Product> Cart { get; set; }
    }
}
