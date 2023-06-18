using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IHMS.Models;

public partial class FinalProContext : DbContext
{
    public FinalProContext()
    {
    }

    public FinalProContext(DbContextOptions<FinalProContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FinalPro;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CCourseId);

            entity.ToTable("Course");

            entity.Property(e => e.CCourseId).HasColumnName("c_course_id");
            entity.Property(e => e.CIntro)
                .HasMaxLength(100)
                .HasColumnName("c_intro");
            entity.Property(e => e.CName)
                .HasMaxLength(50)
                .HasColumnName("c_name");
            entity.Property(e => e.CTeacherId).HasColumnName("c_teacher_id");
            entity.Property(e => e.CType)
                .HasMaxLength(50)
                .HasColumnName("c_type");
            entity.Property(e => e.CVideo)
                .HasMaxLength(100)
                .HasColumnName("c_video");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.SScheduleId).HasName("PK_Table_1");

            entity.ToTable("Schedule");

            entity.Property(e => e.SScheduleId).HasColumnName("s_schedule_id");
            entity.Property(e => e.SCourseId).HasColumnName("s_course_id");
            entity.Property(e => e.SEndTime)
                .HasColumnType("datetime")
                .HasColumnName("s_endTime");
            entity.Property(e => e.SMonth)
                .HasColumnType("date")
                .HasColumnName("s_month");
            entity.Property(e => e.SPoint).HasColumnName("s_point");
            entity.Property(e => e.SScore).HasColumnName("s_score");
            entity.Property(e => e.SStartTime)
                .HasColumnType("datetime")
                .HasColumnName("s_startTime");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TTeacherId).HasName("PK_Table_2");

            entity.ToTable("Teacher");

            entity.Property(e => e.TTeacherId).HasColumnName("t_teacher_id");
            entity.Property(e => e.TApplytime)
                .HasColumnType("datetime")
                .HasColumnName("t_applytime");
            entity.Property(e => e.TCommission).HasColumnName("t_commission");
            entity.Property(e => e.TCondition).HasColumnName("t_condition");
            entity.Property(e => e.TConfirmtime)
                .HasColumnType("datetime")
                .HasColumnName("t_confirmtime");
            entity.Property(e => e.TImage)
                .HasMaxLength(50)
                .HasColumnName("t_image");
            entity.Property(e => e.TIntro)
                .HasMaxLength(200)
                .HasColumnName("t_intro");
            entity.Property(e => e.TLevel).HasColumnName("t_level");
            entity.Property(e => e.TMemberId).HasColumnName("t_member_id");
            entity.Property(e => e.TResume)
                .HasMaxLength(100)
                .HasColumnName("t_resume");
            entity.Property(e => e.TScore).HasColumnName("t_score");
            entity.Property(e => e.TVideo)
                .HasMaxLength(100)
                .HasColumnName("t_video");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
