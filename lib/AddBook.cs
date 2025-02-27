using lib.Data;
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
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            CheckBook checkBook = new CheckBook();
            checkBook.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Некорректные данные.Проверьте данные.Все данные обязательные");
            }
            else
            {
                using (LibDbContext _lib = new LibDbContext())
                {
                    var existingIds = _lib.Books.Select(s => s.Id).ToList();
                    int newId = 1; // Начинаем с 1
                    while (existingIds.Contains(newId))
                    {
                        newId++;
                    }
                    Book newbook = new Book
                    {
                        Id = newId,
                        Name = textBox1.Text,
                        Author = textBox2.Text,
                        Genre = textBox3.Text,
                        YearOfManufacture = dateTimePicker1.Value,
                        Count = (int)numericUpDown1.Value,
                        Cost = int.Parse(textBox4.Text)

                    };
                    _lib.Books.Add(newbook);
                    _lib.SaveChanges();
                    MessageBox.Show("Добавлена");

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            numericUpDown1.Value = 0;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AddBook_Load(object sender, EventArgs e)
        {

        }
    }
}
