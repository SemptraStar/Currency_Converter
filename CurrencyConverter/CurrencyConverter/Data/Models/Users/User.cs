using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Data.Models.Users
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Connection> Connections { get; set; }
    }
}
