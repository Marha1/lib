using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();
        public ICollection<HistoryStudent> HistoryStudents { get; set; } = new List<HistoryStudent>();

    }
}
