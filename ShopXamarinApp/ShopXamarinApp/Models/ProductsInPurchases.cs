using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Models
{
    /// <summary>
    /// Класс продуктов в покупке.
    /// </summary>
    public class ProductsInPurchases
    {
        public int ProductID { get; set; }
        public int PurchaseID { get; set; }
        public int ID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
