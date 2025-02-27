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
            this.FormClosing += Form1_FormClosing; // �������� �� �������
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
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
            // ���������� ������ ��� ������������� ��������
            var result = MessageBox.Show(
                "�� �������, ��� ������ �����?",
                "������������� ������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                // �������� ��������
                e.Cancel = true;
            }
        }
        private void CheckAndAddDebtors()
        {
            using (var context = new LibDbContext())
            {
                if (context.StudentBooks.ToList().Count != 0)
                {


                    // �������� ������� ����
                    DateTime currentDate = DateTime.Now;

                    // ���� �����, ���� �������� ������� ��� ������
                    var overdueBooks = context.StudentBooks
                        .Where(sb => sb.DateOfReturn.ToUniversalTime() < currentDate.ToUniversalTime()) // ���������� DateOfReturn ��� ��������
                        .Include(sb => sb.Student)  // ��������� ������ ��������
                        .Include(sb => sb.Book)     // ��������� ������ �����
                        .ToList();
                    if (overdueBooks.Count == 0)
                    {
                        return;
                    }

                    if (overdueBooks.Count > 0)
                    {

                        // ������������ ��� ������������ �����
                        foreach (var studentBook in overdueBooks)
                        {
                            // ���������, ���� �� ��� ������ � �������� ��� ���� �����
                            bool isAlreadyDebtor = context.Debtors
                                .Any(debtor => debtor.StudentId == studentBook.StudentId && debtor.BookId == studentBook.BookId);

                            // ���� �������� ��� ���� ����� ��� ���, ������� ������
                            if (!isAlreadyDebtor)
                            {
                                var debtor = new Debtor
                                {
                                    StudentId = studentBook.StudentId, // �������
                                    BookId = studentBook.BookId,       // �����
                                    DebtAmount = studentBook.Book.Cost * studentBook.BookCount // ��������� ����� ��� ����
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

                        // ��������� ��������� � ���� ������
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
            �ardIndex card = new �ardIndex();
            card.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            �ardIndex �ard = new �ardIndex();
            �ard.Show();
        }
    }
}
