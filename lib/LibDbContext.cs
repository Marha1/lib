using Microsoft.EntityFrameworkCore;
using lib.Models;

namespace lib.Data
{
    public class LibDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } // Таблица для книг
        public DbSet<Student> Students { get; set; } // Таблица для студентов
        public DbSet<Debtor> Debtors { get; set; } // Таблица для должников (только для отображения)
        public DbSet<StudentBook> StudentBooks { get; set; } // Промежуточная таблица "Студенты-Книги"
        public DbSet<HistoryStudent> History { get; set; } // Промежуточная таблица "Студенты-Книги"



        public LibDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=Lib;username=postgres;password=053352287");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id); // Первичный ключ
                entity.Property(b => b.Author)
                      .IsRequired()
                      .HasMaxLength(100); // Автор обязателен, макс. длина 100 символов
                entity.Property(b => b.Genre)
                      .IsRequired()
                      .HasMaxLength(50); // Жанр обязателен, макс. длина 50 символов
                entity.Property(b => b.Cost)
                      .HasColumnType("decimal(18,2)"); // Тип для денежного значения
                entity.Property(b => b.YearOfManufacture)
                      .IsRequired();
                entity.Property(b => b.Count)
                      .IsRequired()
                      .HasDefaultValue(1); // Количество экземпляров по умолчанию
            });
            ///Конфа для истории
            modelBuilder.Entity<HistoryStudent>(entity =>
            {
                // Первичный ключ
                entity.HasKey(h => h.Id);

                // Связь с таблицей Student (один студент - много историй)
                entity.HasOne(h => h.Student)
                      .WithMany(s => s.HistoryStudents)
                      .HasForeignKey(h => h.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Поля с книгами
                entity.Property(h => h.BookAuthor)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(h => h.BookName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(h => h.BookGenre)
                      .HasMaxLength(100);

                // Поля с датами
                entity.Property(h => h.BorrowDate)
                      .IsRequired();

                entity.Property(h => h.DateOfReturn)
                      .IsRequired(false);

                entity.Property(h => h.DebtHanged)
                      .IsRequired(false);

                entity.Property(h => h.DebtRemoved)
                      .IsRequired(false);

                // Поле с количеством книг
                entity.Property(h => h.BookCount)
                      .IsRequired()
                      .HasDefaultValue(0);
            });

            // Конфигурация Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id); // Первичный ключ
                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(50); // Имя обязательно, макс. длина 50 символов
                entity.Property(s => s.Surname)
                      .IsRequired()
                      .HasMaxLength(50); // Фамилия обязательно
                entity.Property(s => s.LastName)
                      .HasMaxLength(50); // Отчество, необязательное поле
                entity.Property(s => s.Group)
                      .IsRequired()
                      .HasMaxLength(20); // Группа обязательна
                entity.Property(s => s.PhoneNumber)
                      .HasMaxLength(15); // Номер телефона
                entity.Property(s => s.Address)
                      .HasMaxLength(200); // Адрес
            });

            // Конфигурация Debtor
            modelBuilder.Entity<Debtor>(entity =>
            {
                entity.HasKey(dk=>dk.Id);  // Указываем первичный ключ для должника
                entity.Property(d => d.DebtAmount)
                      .HasColumnType("decimal(18,2)"); // Денежный тип
            });

            // Конфигурация StudentBook
            modelBuilder.Entity<StudentBook>(entity =>
            {
                entity.HasKey(sb => sb.Id); // Первичный ключ
                entity.HasOne(sb => sb.Student)
                      .WithMany(s => s.StudentBooks)
                      .HasForeignKey(sb => sb.StudentId)
                      .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
                entity.HasOne(sb => sb.Book)
                      .WithMany()
                      .HasForeignKey(sb => sb.BookId)
                      .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
                entity.Property(sb => sb.BorrowDate)
                      .IsRequired(); // Дата взятия книги обязательна
                entity.Property(sb => sb.DateOfReturn)
                      .IsRequired(); // Дата возврата книги обязательна
            });
        }
    }
}
