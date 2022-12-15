using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodPickerApi.Entities
{
    public partial class foodpickerdbContext : DbContext
    {
        public foodpickerdbContext()
        {
            
        }

        public foodpickerdbContext(DbContextOptions<foodpickerdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Parent> Parents { get; set; } = null!;
        public virtual DbSet<ParentStudent> ParentStudents { get; set; } = null!;
        public virtual DbSet<Preference> Preferences { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.Carbs).HasColumnName("carbs");

                entity.Property(e => e.Fats).HasColumnName("fats");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(191)
                    .HasColumnName("imgURL");

                entity.Property(e => e.Ingredients)
                    .HasMaxLength(191)
                    .HasColumnName("ingredients");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Proteins).HasColumnName("proteins");

                entity.Property(e => e.Type)
                    .HasColumnType("enum('PRIMARY','SECONDARY','DRINK')")
                    .HasColumnName("type");

                entity.Property(e => e.WeightGrams).HasColumnName("weightGrams");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.TeacherId, "Grade_teacherId_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Letter)
                    .HasMaxLength(1)
                    .HasColumnName("letter")
                    .IsFixedLength();

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.TeacherId).HasColumnName("teacherId");

                entity.Property(e => e.breakIndex).HasColumnName("breakIndex");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime(3)")
                    .HasColumnName("date");

                entity.Property(e => e.DishId).HasColumnName("dishId");

                entity.Property(e => e.StudentId).HasColumnName("studentId");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.ToTable("Parent");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Username, "Parent_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(191)
                    .HasColumnName("middleName");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(191)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(191)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(191)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ParentStudent>(entity =>
            {
                entity.ToTable("ParentStudent");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.StudentId).HasColumnName("studentId");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("Preference");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DayOfWeek).HasColumnName("dayOfWeek");

                entity.Property(e => e.DishId).HasColumnName("dishId");

                entity.Property(e => e.StudentId).HasColumnName("studentId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GradeId).HasColumnName("gradeId");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(191)
                    .HasColumnName("middleName");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(191)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Username, "Teacher_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(191)
                    .HasColumnName("middleName");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(191)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(191)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(191)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");

                entity.UseCollation("utf8mb4_unicode_ci");

                entity.HasIndex(e => e.Username, "Worker_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(191)
                    .HasColumnName("middleName");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(191)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnType("enum('WORKER','ADMIN')")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'WORKER'");

                entity.Property(e => e.Surname)
                    .HasMaxLength(191)
                    .HasColumnName("surname");

                entity.Property(e => e.Username)
                    .HasMaxLength(191)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
