using lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lib
{
    public partial class ShowHistory : Form
    {
        ICollection<HistoryStudent> _history = new List<HistoryStudent>();
        public ShowHistory(ICollection<HistoryStudent> history)
        {
            InitializeComponent();
            _history = history;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowHistory_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _history
     .Select(x => new
     {
         x.Id,
         x.BookName,
         x.BookAuthor,
         x.BookGenre,
         x.BookCount,
         x.BorrowDate,
         x.DateOfReturn,
         x.DebtHanged,
         x.DebtRemoved
     })
     .ToList(); // Материализация данных


        }


    }
}
