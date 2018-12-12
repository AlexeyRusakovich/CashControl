using System;
using CashControl.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CashControl.Models
{
    public partial class CashControlContext : DbContext
    {
        public CashControlContext()
        {
        }

        public CashControlContext(DbContextOptions<CashControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CashControl;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Categories)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Currency");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_TransactionType");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Users>().HasData(
                new Users { Login = "admin", Password = EncryptHelper.Encrypt("admin") });

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType { Id = 1, Name = "Income" },
                new TransactionType { Id = 2, Name = "Outcome" });

            modelBuilder.Entity<Currency>().HasData(
                new Currency { Abbreviation = "BYN", FullName = "Belarusian ruble", Id = 1 },
                new Currency { Abbreviation = "USD", FullName = "United States dollar", Id = 2 },
                new Currency { Abbreviation = "EUR", FullName = "European euro", Id = 3 },
                new Currency { Abbreviation = "UAH", FullName = "Ukrainian hryvnia", Id = 4 },
                new Currency { Abbreviation = "RUB", FullName = "Russian ruble", Id = 5 });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Продукты" },
                new Category { Id = 2, Name = "Автомобиль" },
                new Category { Id = 3, Name = "Транспорт" },
                new Category { Id = 4, Name = "Развлечения" },
                new Category { Id = 5, Name = "Забота о себе" },
                new Category { Id = 6, Name = "Отпуск" },
                new Category { Id = 7, Name = "Товары для дома" },
                new Category { Id = 8, Name = "Образование" },
                new Category { Id = 9, Name = "Разное" },
                new Category { Id = 10, Name = "Перевод" },
                new Category { Id = 111, Name = "Бизнес" });
        }
    }
}
