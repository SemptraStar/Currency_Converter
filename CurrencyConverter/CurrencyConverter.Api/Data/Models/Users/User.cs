using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Api.Data.Models.Users
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        public ICollection<Connection> Connections { get; set; }
    }
}
