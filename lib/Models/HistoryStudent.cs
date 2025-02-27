using System;

namespace lib.Models
{
    public class HistoryStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public string BookAuthor { get; set; }
        public string BookName { get; set; }
        public string BookGenre { get; set; }

        private DateTime? _borrowDate;
        private DateTime? _dateOfReturn;

        public DateTime? BorrowDate
        {
            get => _borrowDate;
            set => _borrowDate = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : (DateTime?)null;
        }

        public DateTime? DateOfReturn
        {
            get => _dateOfReturn;
            set => _dateOfReturn = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : (DateTime?)null;
        }

        public int BookCount { get; set; }

        private DateTime? _debtHanged;
        private DateTime? _debtRemoved;

        public DateTime? DebtHanged
        {
            get => _debtHanged;
            set => _debtHanged = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : (DateTime?)null;
        }

        public DateTime? DebtRemoved
        {
            get => _debtRemoved;
            set => _debtRemoved = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : (DateTime?)null;
        }
        public decimal DebtAmount { get; set; } = 0;
        public decimal Amount { get; set; } = 0;

    }
}
