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
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace lib
{
    public partial class СardIndex : Form
    {
        public СardIndex()
        {
            InitializeComponent();
        }

        public void SetButtonVisibility(bool isVisible)
        {
            button2.Visible = isVisible;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }


        private void СardIndex_Load(object sender, EventArgs e)
        {
            using (LibDbContext _context = new LibDbContext())
            {
                var students = _context.Students.ToList().OrderBy(x => x.Id);
                dataGridView1.DataSource = students.Select(s => new { s.Id, s.Name, s.Surname, s.LastName, s.Group, s.PhoneNumber, s.Address }).ToList();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (LibDbContext _context = new LibDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string studentName = selectedRow.Cells["Name"].Value.ToString();
                    string studentSurname = selectedRow.Cells["Surname"].Value.ToString();
                    string group = selectedRow.Cells["Group"].Value.ToString();

                    var student = _context.Students
       .Include(s => s.HistoryStudents) // Жадная загрузка истории
       .FirstOrDefault(s =>
           s.Name == studentName &&
           s.Surname == studentSurname &&
           s.Group == group);

                    if (student != null && student.HistoryStudents.Count > 0)
                    {
                        var history = student.HistoryStudents;
                        ShowHistory show = new ShowHistory(history);
                        show.Show();
                    }
                    else
                    {
                        MessageBox.Show("Нет связанных историй с этим студентом");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student selectedStudent = Get(); // Получаем студента

            if (selectedStudent != null)
            {
                // Открываем Form2 и передаем студента в его конструктор
                Issue form2 = new Issue(selectedStudent);
                form2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Студент не выбран.");
            }
        }

        public Student Get()
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (LibDbContext _context = new LibDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string studentName = selectedRow.Cells["Name"].Value.ToString();
                    string studentSurname = selectedRow.Cells["Surname"].Value.ToString();
                    string group = selectedRow.Cells["Group"].Value.ToString();

                    var student = _context.Students
                        .FirstOrDefault(s =>
                         s.Name == studentName &&
                        s.Surname == studentSurname &&
                         s.Group == group);
                    return student;
                }
            }
            return null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            // Получаем выбранного студента из DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int studentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);  // Предполагаем, что "Id" — это колонка с ID студента

                using (LibDbContext _context = new LibDbContext())
                {
                    // Находим студента в базе данных по его ID
                    var studentToDelete = _context.Students.FirstOrDefault(s => s.Id == studentId);
                    if (studentToDelete != null)
                    {
                        // Удаляем студента
                        _context.Students.Remove(studentToDelete);
                        _context.SaveChanges();

                        // Обновляем данные в DataGridView
                        var students = _context.Students.ToList().OrderBy(x => x.Id);
                        dataGridView1.DataSource = students.Select(s => new { s.Id, s.Name, s.Surname, s.LastName, s.Group, s.PhoneNumber, s.Address }).ToList();
                        MessageBox.Show("Студент успешно удален.");
                    }
                    else
                    {
                        MessageBox.Show("Студент не найден.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите студента для удаления.");
            }
        }
    }
    }

