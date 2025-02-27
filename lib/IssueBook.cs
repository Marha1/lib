using lib.Data;
using lib.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace lib
{
    public partial class IssueBook : Form
    {
        public IssueBook()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void IssueBook_Load(object sender, EventArgs e)
        {
            GetBook();
        }

        private void GetBook()
        {
            using (LibDbContext db = new LibDbContext())
            {
                // Получение списка книг, выданных студентам, с учётом связей
                var books = db.StudentBooks
                              .Include(x => x.Book)
                              .Include(c => c.Student)
                              .OrderBy(b => b.Id)
                              .ToList();

                // Очистка колонок и строк DataGridView перед заполнением
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // Добавление колонок в DataGridView
                dataGridView1.Columns.Add("StudentName", "Имя студента");
                dataGridView1.Columns.Add("Group", "Группа");
                dataGridView1.Columns.Add("BookName", "Книга");
                dataGridView1.Columns.Add("Genre", "Жанр");
                dataGridView1.Columns.Add("BorrowDate", "Дата выдачи");
                dataGridView1.Columns.Add("ReturnDate", "Срок возврата");
                dataGridView1.Columns.Add("BookCount", "Кол-во");

                // Заполнение DataGridView данными
                foreach (var item in books)
                {
                    if (item.BookCount > 0 && item.Student is not null)
                    {
                        dataGridView1.Rows.Add(
                            item.Student.Name,
                            item.Student.Group,
                            item.Book.Name,
                            item.Book.Genre,
                            item.BorrowDate.ToString("dd.MM.yyyy"),
                            item.DateOfReturn.ToString("dd.MM.yyyy"),
                            item.BookCount
                        );
                    }
                }

                // Проверка на наличие данных
                if (dataGridView1.Rows.Count == 0)
                {
                    dataGridView1.Visible = false; // Скрываем DataGridView, если данных нет
                    label1.Text = "Выданных книг нет"; // Лейбл с сообщением
                    label1.Visible = true;

                    button1.Visible = false;
                    button2.Visible = false;
                    numericUpDown1.Visible = false;
                }
                else
                {
                    // Показываем DataGridView и кнопки, если данные есть
                    dataGridView1.Visible = true;
                    label1.Visible = false;

                    button1.Visible = true;
                    button2.Visible = true;
                    numericUpDown1.Visible = true;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (LibDbContext _context = new LibDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string studentName = selectedRow.Cells["StudentName"].Value.ToString();
                    string group = selectedRow.Cells["Group"].Value.ToString();
                    string BookName = selectedRow.Cells["BookName"].Value.ToString();
                    string Genre = selectedRow.Cells["Genre"].Value.ToString();


                    var studentBook = _context.StudentBooks
        .Include(sb => sb.Book) // Включаем данные о книге
        .Include(sb => sb.Student) // Включаем данные о студенте
        .FirstOrDefault(sb =>
            sb.Book.Name == BookName && // Название книги
            sb.Book.Genre == Genre && // Жанр книги
            sb.Student.Name == studentName && // Имя студента
            sb.Student.Group == group); // Группа студента

                    var choose = studentBook.Id;
                    if (choose != 0 || dataGridView1.SelectedRows.Count > 0)
                    {
                        using (LibDbContext db = new LibDbContext())
                        {
                            var books = db.StudentBooks.OrderBy(b => b.Id).Include(x => x.Book).Include(s => s.Student).FirstOrDefault(x => x.Id == choose);

                            if (numericUpDown1.Value != 0)
                            {
                                if (books.BookCount >= (int)numericUpDown1.Value)
                                {
                                    books.BookCount -= (int)numericUpDown1.Value;
                                    books.Book.Count += (int)numericUpDown1.Value;
                                    if (books.BookCount == 0)
                                    {
                                        db.StudentBooks.Remove(books);
                                        if (books.Lost == 0)
                                        {
                                            db.Students.Remove(books.Student);

                                        }
                                        db.SaveChanges();
                                        MessageBox.Show($"Возвращено на склад {numericUpDown1.Value} книг");
                                        dataGridView1.Rows.Clear();
                                        GetBook();
                                    }
                                    else
                                    {
                                        db.SaveChanges();
                                        dataGridView1.Rows.Clear();
                                        MessageBox.Show($"Возвращено на склад {numericUpDown1.Value} книг");
                                        GetBook();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Неверное кол-во возрата книг");
                                }


                            }


                        };

                    }
                    else
                    {
                        MessageBox.Show("Вы не выбрали книгу!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (LibDbContext _context = new LibDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string studentName = selectedRow.Cells["StudentName"].Value.ToString();
                    string group = selectedRow.Cells["Group"].Value.ToString();
                    string BookName = selectedRow.Cells["BookName"].Value.ToString();
                    string Genre = selectedRow.Cells["Genre"].Value.ToString();


                    var studentBook = _context.StudentBooks
        .Include(sb => sb.Book) // Включаем данные о книге
        .Include(sb => sb.Student) // Включаем данные о студенте
        .FirstOrDefault(sb =>
            sb.Book.Name == BookName && // Название книги
            sb.Book.Genre == Genre && // Жанр книги
            sb.Student.Name == studentName && // Имя студента
            sb.Student.Group == group); // Группа студента

                    var choose = studentBook.Id;
                    {
                        using (LibDbContext db = new LibDbContext())
                        {
                            var books = db.StudentBooks.OrderBy(b => b.Id).Include(x => x.Book).Include(s => s.Student).FirstOrDefault(x => x.Id == choose);

                            if (numericUpDown1.Value != 0)
                            {
                                if (books.BookCount >= (int)numericUpDown1.Value)
                                {
                                    books.Lost += (int)numericUpDown1.Value;
                                    if (books.Lost <= books.BookCount)
                                    {
                                        books.BookCount -= books.Lost;
                                        var debtor = new Debtor
                                        {
                                            StudentId = books.Student.Id,
                                            BookId = books.Book.Id,
                                            DebtAmount = books.Book.Cost * books.Lost

                                        };
                                        db.Debtors.Add(debtor);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Потеряно больше чем выдано!");
                                    }
                                    if (books.BookCount == 0)
                                    {
                                        db.StudentBooks.Remove(books);
                                        db.SaveChanges();
                                        MessageBox.Show($"Записано");
                                        dataGridView1.Rows.Clear();
                                        GetBook();
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Clear();
                                        GetBook();
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Неверное кол-во возрата книг");
                            }

                        }

                    }

                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали книгу!");
            }
        }
    }
}
