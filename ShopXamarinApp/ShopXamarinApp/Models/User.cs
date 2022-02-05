using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Models
{
    internal class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DataLastEntry { get; set; }
        public int RoleID { get; set; }
        public float Balance { get; set; }
    }
}
