using lib.Data;
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
    public partial class ChangeBook : Form
    {
        private int _idbook;
        public ChangeBook(int idbook)
        {
            _idbook = idbook;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (LibDbContext _lib = new LibDbContext())
            {
                // Находим книгу по идентификатору
                var book = _lib.Books.FirstOrDefault(x => x.Id == _idbook);
                if (book != null)
                {
                    // Обновляем свойства книги из значений UI-элементов
                    book.Name = textBox1.Text;
                    book.Author = textBox2.Text;
                    book.Genre = textBox3.Text;
                    book.Cost = decimal.TryParse(textBox4.Text, out var cost) ? cost : book.Cost;
                    book.YearOfManufacture = dateTimePicker1.Value;
                    book.Count = (int)numericUpDown1.Value;

                    // Сохраняем изменения в базе данных
                    _lib.SaveChanges();

                    MessageBox.Show("Книга успешно обновлена!");
                    loadbook();
                }
                else
                {
                    MessageBox.Show("Книга не найдена!");
                }
            }
        }

        private void ChangeBook_Load(object sender, EventArgs e)
        {
            loadbook();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            CheckBook book = new CheckBook();
            book.Show();
        }
        private void loadbook()
        {
            using (LibDbContext _lib = new LibDbContext())
            {
                var book = _lib.Books.FirstOrDefault(x => x.Id == _idbook);
                if (book != null)
                {
                    textBox1.Text = book.Name;
                    textBox2.Text = book.Author;
                    textBox3.Text = book.Genre;
                    textBox4.Text = book.Cost.ToString();
                    dateTimePicker1.Value = book.YearOfManufacture;
                    numericUpDown1.Value = book.Count;
                }
            }
        }
    }
}
