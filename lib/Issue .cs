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
    public partial class Issue : Form
    {
        private static int _choose;
        private Student _student;

        // Конструктор, принимающий студента
        public Issue(Student student)
        {
            InitializeComponent();
            _student = student;

        }
        public Issue(int choose)
        {
            InitializeComponent();
            _choose = choose;
        }

      

        private void Issue_Load(object sender, EventArgs e)
        {
            if(_student is not null)
            {
                textBox1.Text = _student.Name;
                textBox2.Text = _student.Surname;
                textBox3.Text = _student?.Surname;
                textBox4.Text = _student.Group;
                textBox5.Text = _student?.PhoneNumber;
                textBox6.Text = _student.Address;


            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                using (LibDbContext _lib = new LibDbContext())
                {
                    var book = _lib.Books.FirstOrDefault(x => x.Id == _choose);

                    if (book.Count >= (int)numericUpDown1.Value)
                    {
                        // Проверка на пустые поля ввода
                        if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
                        {
                            MessageBox.Show("Проверьте все данные!");
                        }
                        else
                        {
                            // Проверка на наличие студента среди должников с учетом имени, фамилии и группы
                            var debtorExists = _lib.Debtors
                                .Any(d => d.Student.Name == textBox1.Text
                                          && d.Student.Surname == textBox2.Text
                                          && d.Student.Group == textBox4.Text);

                            if (debtorExists)
                            {
                                MessageBox.Show("Этот студент уже является должником.");
                                return; // Прерываем выполнение метода, если студент уже должник
                            }

                            // Нахождение минимального свободного Id
                            var existingIds = _lib.Students.Select(s => s.Id).ToList();
                            int newId = 1; // Начинаем с 1
                            while (existingIds.Contains(newId))
                            {
                                newId++;
                            }

                            var existingStudent = _lib.Students.FirstOrDefault(s =>
        s.Name == textBox1.Text &&
        s.Surname == textBox2.Text &&
        s.Group == textBox4.Text);

                            if (existingStudent == null)
                            {
                                // Создание нового студента с найденным Id
                                var student = new Student
                                {
                                    Id = newId, // Устанавливаем минимальный свободный Id
                                    Name = textBox1.Text,
                                    Surname = textBox2.Text,
                                    LastName = textBox3.Text,
                                    Group = textBox4.Text,
                                    PhoneNumber = textBox5.Text,
                                    Address = textBox6.Text,
                                };

                                // Добавление нового студента в базу данных
                                _lib.Students.Add(student);
                                existingStudent = student;
                                // Сохранение изменений
                                _lib.SaveChanges();
                                MessageBox.Show("Студент успешно добавлен!");
                            }
                            else
                            {


                            }
                            existingIds = _lib.StudentBooks.Select(s => s.Id).ToList();
                            newId = 1;
                            while (existingIds.Contains(newId))
                            {
                                newId++;
                            }

                            var studentBooks = new StudentBook
                            {
                                Id = newId, // Устанавливаем минимальный свободный Id
                                BookId = book.Id,
                                Book = book,
                                Student = existingStudent,
                                StudentId = existingStudent.Id,
                                BorrowDate = DateTime.Now.AddTicks(-DateTime.Now.Ticks % TimeSpan.TicksPerSecond),
                                DateOfReturn = dateTimePicker1.Value.ToUniversalTime(),
                                BookCount = (int)numericUpDown1.Value,
                            };

                            book.Count -= (int)numericUpDown1.Value;
                            _lib.StudentBooks.Add(studentBooks);
                            _lib.SaveChanges();
                            MessageBox.Show("Выдано");
                            var history = new HistoryStudent()
                            {
                                Student = existingStudent,
                                StudentId = existingStudent.Id,
                                BookAuthor = book.Author,
                                BookName = book.Name,
                                BookCount = (int)numericUpDown1.Value,
                                BorrowDate = DateTime.Now.AddTicks(-DateTime.Now.Ticks % TimeSpan.TicksPerSecond),
                                DateOfReturn = dateTimePicker1.Value.ToUniversalTime(),
                                BookGenre = book.Genre,
                            };
                            _lib.History.Add(history);
                            _lib.SaveChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Такого кол-ва книг нет.Всего доступно:{book.Count}");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка.Сообщите Администратору", ex.Message);
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CheckBook check = new CheckBook();
            check.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            СardIndex  cz = new СardIndex();
            cz.SetButtonVisibility(true);
            cz.Show();
            
        }
    }
}
