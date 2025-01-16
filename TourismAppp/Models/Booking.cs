using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TourismAppp.Models
{
    public class Booking
    {
        [PrimaryKey, AutoIncrement]
        public int BookingID { get; set; }

        [NotNull]
        public int UserID { get; set; } // Foreign Key for the user

        [NotNull]
        public int VacationID { get; set; } // Foreign Key for the vacation

        [NotNull]
        public DateTime BookingDate { get; set; } // Date when the booking was made

        [NotNull]
        public DateTime StartDate { get; set; } // Start date of the vacation

        [NotNull]
        public DateTime EndDate { get; set; } // End date of the vacation

        [NotNull]
        public int NumberOfPeople { get; set; } // Number of people for the booking

        [NotNull]
        public string Status { get; set; }


    }
}
