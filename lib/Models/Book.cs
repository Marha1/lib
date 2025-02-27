using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        private DateTime _yearOfManufacture;

        public DateTime YearOfManufacture
        {
            get => _yearOfManufacture;
            set => _yearOfManufacture = DateTime.SpecifyKind(value, DateTimeKind.Utc); // Указываем формат UTC
        }
        public decimal Cost { get; set; } = 500;
        public int Count { get; set; }
    }
}
