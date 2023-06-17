using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IHMS.Models;

public partial class IfmsContext : DbContext
{
    public IfmsContext()
    {
    }

    public IfmsContext(DbContextOptions<IfmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IFMS;Integrated Security=True;Trust Server Certificate=True ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MMemberId).HasName("PK__Members__BF51BC45A0FE5B6F");

            entity.HasIndex(e => e.MAccount, "UQ__Members__80FC9F7945A6A16C").IsUnique();

            entity.HasIndex(e => e.MEmail, "UQ__Members__D12C572AFF31B369").IsUnique();

            entity.Property(e => e.MMemberId).HasColumnName("m_member_id");
            entity.Property(e => e.MAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("m_account");
            entity.Property(e => e.MAllergyDescription)
                .IsUnicode(false)
                .HasColumnName("m_allergy_description");
            entity.Property(e => e.MAvatarImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("m_avatar_image");
            entity.Property(e => e.MBirthday)
                .HasColumnType("date")
                .HasColumnName("m_birthday");
            entity.Property(e => e.MDiseaseDescription)
                .IsUnicode(false)
                .HasColumnName("m_disease_description");
            entity.Property(e => e.MEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_email");
            entity.Property(e => e.MGender).HasColumnName("m_gender");
            entity.Property(e => e.MLoginTime)
                .HasColumnType("datetime")
                .HasColumnName("m_login_time");
            entity.Property(e => e.MMaritalStatus).HasColumnName("m_marital_status");
            entity.Property(e => e.MName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_name");
            entity.Property(e => e.MNickname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_nickname");
            entity.Property(e => e.MOccupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_occupation");
            entity.Property(e => e.MPassword)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("m_password");
            entity.Property(e => e.MPermission).HasColumnName("m_permission");
            entity.Property(e => e.MPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_phone");
            entity.Property(e => e.MResidentialCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_residential_city");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
