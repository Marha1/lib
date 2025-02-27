using lib.Data;
using lib.Models;
using Microsoft.EntityFrameworkCore;

namespace lib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing; // Подписка на событие
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckBook check = new CheckBook();
            check.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DebtorCheck check = new DebtorCheck();
            check.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckAndAddDebtors();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Показываем диалог для подтверждения закрытия
            var result = MessageBox.Show(
                "Вы уверены, что хотите выйти?",
                "Подтверждение выхода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                // Отменяем закрытие
                e.Cancel = true;
            }
        }
        private void CheckAndAddDebtors()
        {
            using (var context = new LibDbContext())
            {
                if (context.StudentBooks.ToList().Count != 0)
                {


                    // Получаем текущую дату
                    DateTime currentDate = DateTime.Now;

                    // Ищем книги, дата возврата которых уже прошла
                    var overdueBooks = context.StudentBooks
                        .Where(sb => sb.DateOfReturn.ToUniversalTime() < currentDate.ToUniversalTime()) // Используем DateOfReturn для проверки
                        .Include(sb => sb.Student)  // Загружаем данные студента
                        .Include(sb => sb.Book)     // Загружаем данные книги
                        .ToList();
                    if (overdueBooks.Count == 0)
                    {
                        return;
                    }

                    if (overdueBooks.Count > 0)
                    {

                        // Обрабатываем все просроченные книги
                        foreach (var studentBook in overdueBooks)
                        {
                            // Проверяем, есть ли уже запись о должнике для этой книги
                            bool isAlreadyDebtor = context.Debtors
                                .Any(debtor => debtor.StudentId == studentBook.StudentId && debtor.BookId == studentBook.BookId);

                            // Если должника для этой книги еще нет, создаем нового
                            if (!isAlreadyDebtor)
                            {
                                var debtor = new Debtor
                                {
                                    StudentId = studentBook.StudentId, // Студент
                                    BookId = studentBook.BookId,       // Книга
                                    DebtAmount = studentBook.Book.Cost * studentBook.BookCount // Стоимость книги как долг
                                };


                                var his = new HistoryStudent()
                                {
                                    StudentId = studentBook.StudentId,
                                    BookAuthor = studentBook.Book.Author,
                                    BookCount = studentBook.BookCount,
                                    BookGenre = studentBook.Book.Genre,
                                    DebtAmount = debtor.DebtAmount,
                                    BookName = studentBook.Book.Name,
                                    BorrowDate = DateTime.Now,
                                };
                                context.Debtors.Add(debtor);
                                context.History.Add(his);
                                context.StudentBooks.Remove(studentBook);
                                context.SaveChanges();

                            }
                        }

                        // Сохраняем изменения в базе данных
                        context.SaveChanges();
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            IssueBook issueBook = new IssueBook();
            issueBook.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            СardIndex card = new СardIndex();
            card.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            СardIndex сard = new СardIndex();
            сard.Show();
        }
    }
}
