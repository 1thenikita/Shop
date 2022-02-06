using System;
using System.Collections.Generic;
using System.Text;

namespace ShopXamarinApp.Models
{
    /// <summary>
    /// Класс пользователя.
    /// </summary>
    public class User
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public Nullable<DateTime> DataLastEntry { get; set; }
            public int RoleID { get; set; }
            public float Balance { get; set; }
            public virtual Role Role { get; set; }
    }
}
