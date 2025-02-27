using lib.Data;
using Microsoft.EntityFrameworkCore;
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
    public partial class CheckBook : Form
    {
        public CheckBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (LibDbContext _context = new LibDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string bookname = selectedRow.Cells["Name"].Value.ToString();
                    string bookAuthor = selectedRow.Cells["Author"].Value.ToString();
                    string BookGenre = selectedRow.Cells["Genre"].Value.ToString();
                   var book= _context.Books.FirstOrDefault(s =>
           s.Name == bookname &&
           s.Author == bookAuthor &&
           s.Genre == BookGenre);

                    this.Close();
                    Issue issue = new Issue(book.Id);
                    issue.Show();


                }


            }

            else
            {
                MessageBox.Show("Вы не выбрали книгу!");
            }
        }

        private void CheckBook_Load(object sender, EventArgs e)
        {
            GetBook();

        }

        private void GetBook()
        {
            button1.Visible = true;
            using (LibDbContext db = new LibDbContext())
            {
                // Получение списка книг из базы данных, отсортированных по ID
                var books = db.Books.OrderBy(b => b.Id).ToList();

                // Очистка колонок и строк DataGridView перед заполнением
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // Добавление колонок в DataGridView
                dataGridView1.Columns.Add("Name", "Название");
                dataGridView1.Columns.Add("Author", "Автор");
                dataGridView1.Columns.Add("Genre", "Жанр");
                dataGridView1.Columns.Add("Year", "Год");
                dataGridView1.Columns.Add("Count", "Кол-во");
                dataGridView1.Columns.Add("Cost", "Стоимость");

                // Заполнение DataGridView данными из базы
                foreach (var item in books)
                {
                    dataGridView1.Rows.Add(
                        item.Name,
                        item.Author,
                        item.Genre,
                        item.YearOfManufacture.ToString("dd.MM.yyyy"),
                        item.Count,
                        item.Cost.ToString("0.00")
                    );
                }

                // Если книг нет
                // Проверка на наличие данных
                if (dataGridView1.Rows.Count == 0)
                {
                    // Если строк нет, выводим сообщение и скрываем кнопки
                    dataGridView1.Visible = false; // Прячем DataGridView
                    label1.Text = "К сожалению, свободных книг нет"; // Добавляем лейбл с сообщением
                    label1.Visible = true;
                    button1.Visible = false;
                    button3.Visible = false;
                }
                else
                {
                    // Если строки есть, отображаем таблицу и кнопки
                    dataGridView1.Visible = true;
                    label1.Visible = false; // Скрываем сообщение
                    button1.Visible = true;
                    button3.Visible = true;
                }

            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            AddBook book = new AddBook();
            book.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (LibDbContext _context = new LibDbContext())
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                // Получаем данные из выбранной строки
                string bookname = selectedRow.Cells["Name"].Value.ToString();
                string bookAuthor = selectedRow.Cells["Author"].Value.ToString();
                string BookGenre = selectedRow.Cells["Genre"].Value.ToString();
               var book= _context.Books.FirstOrDefault(s =>
       s.Name == Name &&
       s.Author == bookAuthor &&
       s.Genre == BookGenre);


                this.Close();
                ChangeBook changeBook = new ChangeBook(book.Id);
                changeBook.Show();
            }
            
        }
    }
}
