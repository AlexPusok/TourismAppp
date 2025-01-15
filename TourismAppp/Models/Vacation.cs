using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TourismAppp.Models
{
    public class Vacation
    {
        [PrimaryKey, AutoIncrement]
        public int VacationID { get; set; }

        [NotNull]
        public string Title { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull]
        public string Location { get; set; }

        [NotNull]
        public decimal Price { get; set; }

        [NotNull]
        public int DurationDays { get; set; }

        [NotNull]
        public DateTime StartDate { get; set; }
        [NotNull]
        public DateTime EndDate { get; set; }
    }
}
