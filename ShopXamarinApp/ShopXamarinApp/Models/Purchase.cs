using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Models
{
    /// <summary>
    /// Класс покупки.
    /// </summary>
    public class Purchase
    {
        public int ID { get; set; }
        public DateTime DatePurchase { get; set; }
        public float TotalMoney { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
