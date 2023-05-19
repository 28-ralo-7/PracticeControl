using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PracticeControl.WebAPI.Database;

public partial class ProductionPracticeControlContext : DbContext
{
    public ProductionPracticeControlContext()
    {
    }

    public ProductionPracticeControlContext(DbContextOptions<ProductionPracticeControlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Practice> Practices { get; set; }

    public virtual DbSet<Practiceschedule> Practiceschedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductionPracticeControl;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdPractice).HasColumnName("id_practice");
            entity.Property(e => e.IdStudent).HasColumnName("id_student");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Ispresent).HasColumnName("ispresent");
            entity.Property(e => e.Photo)
                .HasColumnType("character varying")
                .HasColumnName("photo");

            entity.HasOne(d => d.IdPracticeNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.IdPractice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_schedule_id");

            entity.HasOne(d => d.IdStudentNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.IdStudent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Login, "login_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(100)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Passwordsalt).HasColumnName("passwordsalt");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Practice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("practice_pkey");

            entity.ToTable("practice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(50)
                .HasColumnName("abbreviation");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Practicemodule)
                .HasMaxLength(100)
                .HasColumnName("practicemodule");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .HasColumnName("specialty");
        });

        modelBuilder.Entity<Practiceschedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("practiceschedule_pkey");

            entity.ToTable("practiceschedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdPractice).HasColumnName("id_practice");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Startdate).HasColumnName("startdate");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Practiceschedules)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employee_id");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Practiceschedules)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_groups_id");

            entity.HasOne(d => d.IdPracticeNavigation).WithMany(p => p.Practiceschedules)
                .HasForeignKey(d => d.IdPractice)
                .HasConstraintName("fk_practice_id");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_pkey");

            entity.ToTable("student");

            entity.HasIndex(e => e.Login, "login").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(100)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Passwordsalt).HasColumnName("passwordsalt");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
