using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Models
{
    /// <summary>
    /// Класс продукта.
    /// </summary>
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsActual { get; set; }
        [JsonIgnore]
        public string IsActualString { get => IsActual ? "Актуально": "Не актуально"; }
        public int UserID { get; set; }
        public virtual User User { get; set; }

    }
}
