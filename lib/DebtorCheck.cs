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

namespace lib
{
    public partial class DebtorCheck : Form
    {
        public DebtorCheck()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void DebtorCheck_Load(object sender, EventArgs e)
        {
            GetDeb();
        }

        private void GetDeb()
        {
            button1.Visible = true;
            using (LibDbContext db = new LibDbContext())
            {
                var debtors = db.Debtors
                                .Include(x => x.Book)
                                .Include(c => c.Student)
                                .OrderBy(b => b.Id);

                // Заголовок таблицы
                listBox1.Items.Add($"{"Имя",-20} {"Группа",-15} {"Телефон",-15} {"Адрес",-25} {"Книга",-30} {"Долг",-7}");
                listBox1.Items.Add(new string('-', 120)); // Разделитель

                foreach (var item in debtors)
                {
                    if ( item.DebtAmount > 0 && item.Student is not null)
                    {
                        // Форматированный вывод данных о должнике
                        var address = item.Student.Address.Length > 25 ? item.Student.Address.Substring(0, 22) + "..." : item.Student.Address;
                        var bookName = item.Book.Name.Length > 30 ? item.Book.Name.Substring(0, 27) + "..." : item.Book.Name;

                        listBox1.Items.Add($"{item.Student.Name,-20} {item.Student.Group,-15} {item.Student.PhoneNumber,-15} {address,-25} {bookName,-30} {item.DebtAmount,-7}");
                    }
                }

                // Если список пуст
                if (listBox1.Items.Count == 2) // Только заголовок и разделитель
                {
                    listBox1.Items.Clear();
                    button1.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = false;
                    listBox1.Items.Add("Должников нет");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var choose = listBox1.SelectedIndex - 1;
            if (choose != 0 || listBox1.SelectedIndex != -1)
            {

                using (LibDbContext db = new LibDbContext())
                {
                    try
                    {

                        var inputcost = decimal.Parse(textBox1.Text);
                          var deb = db.Debtors.OrderBy(b => b.Id).Include(x => x.Book).Include(s => s.Student).FirstOrDefault(x => x.Id == choose);
                         if (deb.DebtAmount- inputcost == 0)
                         {
                            var histor = new HistoryStudent()
                            {
                                StudentId = deb.StudentId,
                                Student = deb.Student,
                                DebtAmount = 0,
                                Amount = inputcost,
                                DebtRemoved = DateTime.Now.AddTicks(-DateTime.Now.Ticks % TimeSpan.TicksPerSecond)
                            
                            };
                            db.History.Add(histor);
                            db.SaveChanges();
                            Del(db, deb);

                        }
                        else
                        {
                            if (deb.DebtAmount< inputcost)
                            {
                                Del(db, deb);
                                var sdacha = deb.DebtAmount - inputcost;
                                var histor = new HistoryStudent()
                                {
                                    StudentId = deb.StudentId,
                                    Student = deb.Student,
                                    DebtAmount = 0,
                                    Amount = inputcost,
                                    DebtRemoved = DateTime.Now.AddTicks(-DateTime.Now.Ticks % TimeSpan.TicksPerSecond)

                                };
                                db.History.Add(histor);
                                db.SaveChanges();
                                MessageBox.Show($"Cумма сдачи:{Math.Abs( sdacha)}");

                            }
                            if (deb.DebtAmount > inputcost)
                            {
                                deb.DebtAmount -= inputcost;
                                var histor = new HistoryStudent()
                                {
                                    StudentId = deb.StudentId,
                                    Student = deb.Student,
                                    DebtAmount = deb.DebtAmount,
                                    Amount = inputcost,
                                    DebtRemoved = DateTime.Now.AddTicks(-DateTime.Now.Ticks % TimeSpan.TicksPerSecond)

                                };
                                db.History.Add(histor);
                                db.SaveChanges();
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Ошибка с вводом суммы!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали должника");
            }
            listBox1.Items.Clear();
            GetDeb();
        }
        private void Del(LibDbContext db,Debtor deb)
        {
            db.Debtors.Remove(deb);
            db.SaveChanges();
        }
    }
   
}
