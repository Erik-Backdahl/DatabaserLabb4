using System;
using System.Collections.Generic;
using ErikLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace ErikLabb3.Data;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourseRecord> StudentCourseRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NLKRS6N;Initial Catalog=School;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__classes__FDF47986F78EBD23");

            entity.ToTable("classes");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassSize).HasColumnName("class_size");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.HomeRoom)
                .HasMaxLength(50)
                .HasColumnName("home_room");
            entity.Property(e => e.HomeTeacherId).HasColumnName("home_teacher_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.HomeRoomNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.HomeRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("home_room_fk");

            entity.HasOne(d => d.HomeTeacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.HomeTeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("home_teacher_id_fk");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__classroo__19675A8A6C4DC70C");

            entity.ToTable("classrooms");

            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .HasColumnName("room_id");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__courses__8F1EF7AED0BFA800");

            entity.ToTable("courses");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.MainClassroomId)
                .HasMaxLength(50)
                .HasColumnName("main_classroom_id");
            entity.Property(e => e.MainTeacherId).HasColumnName("main_teacher_id");

            entity.HasOne(d => d.MainClassroom).WithMany(p => p.Courses)
                .HasForeignKey(d => d.MainClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courses_main_classroom_id_fk");

            entity.HasOne(d => d.MainTeacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.MainTeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("courses_main_teacher_id_fk");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Grade1).HasName("PK__grades__28A831772E874A81");

            entity.ToTable("grades");

            entity.Property(e => e.Grade1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("grade");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staff__1963DD9C326C3DE2");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Substitute)
                .HasDefaultValue(false)
                .HasColumnName("substitute");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__students__2A33069A3A827659");

            entity.ToTable("students");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PersonalNumber).HasColumnName("personal_number");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("class_id_fk");
        });

        modelBuilder.Entity<StudentCourseRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__student___3213E83F24C1D816");

            entity.ToTable("student_course_record");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.GradeSetDate).HasColumnName("grade_set_date");
            entity.Property(e => e.GradedBy).HasColumnName("graded_by");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StudentFinalGrade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_final_grade");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourseRecords)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_id_fk");

            entity.HasOne(d => d.GradedByNavigation).WithMany(p => p.StudentCourseRecords)
                .HasForeignKey(d => d.GradedBy)
                .HasConstraintName("graded_by_fk");

            entity.HasOne(d => d.StudentFinalGradeNavigation).WithMany(p => p.StudentCourseRecords)
                .HasForeignKey(d => d.StudentFinalGrade)
                .HasConstraintName("grades_fk");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourseRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
