using System;

namespace lib.Models
{
    public class StudentBook
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int BookId { get; set; }
        public int BookCount { get; set; }
        public Book Book { get; set; }
        public int Lost { get; set; } = 0;
        private DateTime _borrowDate;
        private DateTime _dateOfReturn;

        public DateTime BorrowDate
        {
            get => _borrowDate;
            set => _borrowDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }

        public DateTime DateOfReturn
        {
            get => _dateOfReturn;
            set => _dateOfReturn = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }
    }
}
