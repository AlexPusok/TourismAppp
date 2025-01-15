using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace TourismAppp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int userID { get; set; }

        [NotNull, Unique]
        public string Username { get; set; }
        [NotNull, Unique]
        public string Email { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public string Role { get; set; }
    }
}
