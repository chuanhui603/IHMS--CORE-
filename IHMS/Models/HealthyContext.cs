using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Final.Models;

public partial class HealthyContext : DbContext
{
    public HealthyContext()
    {
    }

    public HealthyContext(DbContextOptions<HealthyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CourseOrder> CourseOrders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PointRecord> PointRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Healthy;Integrated Security=True;\nInitial Catalog=Healthy;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CCartId);

            entity.ToTable("Cart");

            entity.Property(e => e.CCartId).HasColumnName("c_cart_id");
            entity.Property(e => e.CCreatetime)
                .HasColumnType("datetime")
                .HasColumnName("c_createtime");
            entity.Property(e => e.CMemberId).HasColumnName("c_member_id");
            entity.Property(e => e.CScheduleId).HasColumnName("c_schedule_id");
            entity.Property(e => e.CUpdatetime)
                .HasColumnType("datetime")
                .HasColumnName("c_updatetime");
        });

        modelBuilder.Entity<CourseOrder>(entity =>
        {
            entity.HasKey(e => e.CoCourseorderId);

            entity.ToTable("CourseOrder");

            entity.Property(e => e.CoCourseorderId).HasColumnName("co_courseorder_id");
            entity.Property(e => e.CoCreatetime)
                .HasColumnType("datetime")
                .HasColumnName("co_createtime");
            entity.Property(e => e.CoMemberId).HasColumnName("co_member_id");
            entity.Property(e => e.CoPointstotal).HasColumnName("co_pointstotal");
            entity.Property(e => e.CoReason)
                .HasMaxLength(200)
                .HasColumnName("co_reason");
            entity.Property(e => e.CoState)
                .HasMaxLength(50)
                .HasColumnName("co_state");
            entity.Property(e => e.CoUpdatetime)
                .HasColumnType("datetime")
                .HasColumnName("co_updatetime");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OOrderdetailId);

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OOrderdetailId).HasColumnName("o_orderdetail_id");
            entity.Property(e => e.OCourseorderId).HasColumnName("o_courseorder_id");
            entity.Property(e => e.OCreatetime)
                .HasColumnType("datetime")
                .HasColumnName("o_createtime");
            entity.Property(e => e.OScheduleId).HasColumnName("o_schedule_id");
            entity.Property(e => e.OUpdatetime)
                .HasColumnType("datetime")
                .HasColumnName("o_updatetime");
        });

        modelBuilder.Entity<PointRecord>(entity =>
        {
            entity.HasKey(e => e.PPointrecordId);

            entity.ToTable("PointRecord");

            entity.Property(e => e.PPointrecordId).HasColumnName("p_pointrecord_id");
            entity.Property(e => e.PBankNumber).HasColumnName("p_bank_number");
            entity.Property(e => e.PCount).HasColumnName("p_count");
            entity.Property(e => e.PCreatetime)
                .HasColumnType("datetime")
                .HasColumnName("p_createtime");
            entity.Property(e => e.PMemberId).HasColumnName("p_member_id");
            entity.Property(e => e.PUpdatetime)
                .HasColumnType("datetime")
                .HasColumnName("p_updatetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
