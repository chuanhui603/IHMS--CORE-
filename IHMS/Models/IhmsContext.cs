using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IHMS.Models;

public partial class IhmsContext : DbContext
{
    public IhmsContext()
    {
    }

    public IhmsContext(DbContextOptions<IhmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CourseOrder> CourseOrders { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<HealthInfo> HealthInfos { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<PointRecord> PointRecords { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Water> Water { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IHMS;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AMemberId).HasName("PK__Allergie__FE05428828005DA4");

            entity.Property(e => e.AMemberId)
                .ValueGeneratedNever()
                .HasColumnName("a_member_id");
            entity.Property(e => e.AAllergyDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("a_allergy_description");

            entity.HasOne(d => d.AMember).WithOne(p => p.Allergy)
                .HasForeignKey<Allergy>(d => d.AMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Allergies__a_mem__29572725");
        });

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

        modelBuilder.Entity<Diet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Diet");

            entity.Property(e => e.DDate)
                .HasColumnType("date")
                .HasColumnName("d_date");
            entity.Property(e => e.DDietId).HasColumnName("d_diet_id");
            entity.Property(e => e.DImage)
                .HasMaxLength(150)
                .HasColumnName("d_image");
            entity.Property(e => e.DPlanId).HasColumnName("d_plan_id");
            entity.Property(e => e.DRegisterdate)
                .HasColumnType("datetime")
                .HasColumnName("d_registerdate");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DietDetail");

            entity.Property(e => e.DCalories).HasColumnName("d_calories");
            entity.Property(e => e.DDecription)
                .HasMaxLength(100)
                .HasColumnName("d_decription");
            entity.Property(e => e.DFoodType)
                .HasMaxLength(50)
                .HasColumnName("d_food_type");
            entity.Property(e => e.DName)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("d_name");
            entity.Property(e => e.DType)
                .HasMaxLength(50)
                .HasColumnName("d_type");
            entity.Property(e => e.DdDietDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("dd_diet_detail_id");
            entity.Property(e => e.DdDietId).HasColumnName("dd_diet_id");
        });

        modelBuilder.Entity<HealthInfo>(entity =>
        {
            entity.HasKey(e => e.HMemberId).HasName("PK__HealthIn__D1BAB1FCB18660F9");

            entity.ToTable("HealthInfo");

            entity.Property(e => e.HMemberId)
                .ValueGeneratedNever()
                .HasColumnName("h_member_id");
            entity.Property(e => e.HBloodPressure)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("h_blood_pressure");
            entity.Property(e => e.HBmi)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("h_bmi");
            entity.Property(e => e.HBodyFatPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("h_body_fat_percentage");
            entity.Property(e => e.HDateEntered)
                .HasColumnType("datetime")
                .HasColumnName("h_date_entered");
            entity.Property(e => e.HHeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("h_height");
            entity.Property(e => e.HWeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("h_weight");

            entity.HasOne(d => d.HMember).WithOne(p => p.HealthInfo)
                .HasForeignKey<HealthInfo>(d => d.HMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthInf__h_mem__267ABA7A");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.MhMemberId).HasName("PK__MedicalH__35599D541309767A");

            entity.ToTable("MedicalHistory");

            entity.Property(e => e.MhMemberId)
                .ValueGeneratedNever()
                .HasColumnName("mh_member_id");
            entity.Property(e => e.MhDiseaseDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mh_disease_description");

            entity.HasOne(d => d.MhMember).WithOne(p => p.MedicalHistory)
                .HasForeignKey<MedicalHistory>(d => d.MhMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalHi__mh_me__2C3393D0");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MMemberId).HasName("PK__Members__BF51BC45B3902C63");

            entity.Property(e => e.MMemberId)
                .ValueGeneratedNever()
                .HasColumnName("m_member_id");
            entity.Property(e => e.MAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("m_account");
            entity.Property(e => e.MAvatarImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("m_avatar_image");
            entity.Property(e => e.MBirthday)
                .HasColumnType("date")
                .HasColumnName("m_birthday");
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
            entity.Property(e => e.MPoints).HasColumnName("m_points");
            entity.Property(e => e.MResidentialCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_residential_city");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OOrderId);

            entity.Property(e => e.OOrderId).HasColumnName("o_order_id");
            entity.Property(e => e.OCourse)
                .HasMaxLength(50)
                .HasColumnName("o_course");
            entity.Property(e => e.OMemberId).HasColumnName("o_member_id");
            entity.Property(e => e.ONote1)
                .HasMaxLength(50)
                .HasColumnName("o_note1");
            entity.Property(e => e.ONote2)
                .HasMaxLength(50)
                .HasColumnName("o_note2");
            entity.Property(e => e.ONote3)
                .HasMaxLength(50)
                .HasColumnName("o_note3");
            entity.Property(e => e.OOrderDate)
                .HasColumnType("datetime")
                .HasColumnName("o_orderDate");
            entity.Property(e => e.OPoints).HasColumnName("o_points");
            entity.Property(e => e.OScheduleId).HasColumnName("o_schedule_id");
            entity.Property(e => e.OTeacherId).HasColumnName("o_teacher_id");
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

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PPlanId).HasName("PK_plan");

            entity.ToTable("Plan");

            entity.Property(e => e.PPlanId)
                .ValueGeneratedNever()
                .HasColumnName("p_plan_id");
            entity.Property(e => e.PBodyPercentage).HasColumnName("p_body_percentage");
            entity.Property(e => e.PEndDate)
                .HasColumnType("date")
                .HasColumnName("p_end_date");
            entity.Property(e => e.PMemberId).HasColumnName("p_member_id");
            entity.Property(e => e.PRegisterdate)
                .HasColumnType("datetime")
                .HasColumnName("p_registerdate");
            entity.Property(e => e.PWeight).HasColumnName("p_weight");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => e.PPointId);

            entity.Property(e => e.PPointId).HasColumnName("p_point_id");
            entity.Property(e => e.PCount).HasColumnName("p_count");
            entity.Property(e => e.PDatetime)
                .HasColumnType("datetime")
                .HasColumnName("p_datetime");
            entity.Property(e => e.PMemberId).HasColumnName("p_member_id");
            entity.Property(e => e.PNote1)
                .HasMaxLength(50)
                .HasColumnName("p_note1");
            entity.Property(e => e.PNote2)
                .HasMaxLength(50)
                .HasColumnName("p_note2");
            entity.Property(e => e.PNote3)
                .HasMaxLength(50)
                .HasColumnName("p_note3");
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

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.SScheduleId);

            entity.ToTable("Schedule");

            entity.Property(e => e.SScheduleId).HasColumnName("s_schedule_id");
            entity.Property(e => e.SBooking).HasColumnName("s_booking");
            entity.Property(e => e.SEndTime)
                .HasColumnType("datetime")
                .HasColumnName("s_endTime");
            entity.Property(e => e.SStartTime)
                .HasColumnType("datetime")
                .HasColumnName("s_startTime");
            entity.Property(e => e.STeacherId).HasColumnName("s_teacher_id");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sport");

            entity.Property(e => e.SDate)
                .HasColumnType("date")
                .HasColumnName("s_date");
            entity.Property(e => e.SDescription)
                .HasMaxLength(50)
                .HasColumnName("s_description");
            entity.Property(e => e.SImage)
                .HasMaxLength(50)
                .HasColumnName("s_image");
            entity.Property(e => e.SName)
                .HasMaxLength(50)
                .HasColumnName("s_name");
            entity.Property(e => e.SNumber).HasColumnName("s_number");
            entity.Property(e => e.SPlanId).HasColumnName("s_plan_id");
            entity.Property(e => e.SRegisterdate)
                .HasColumnType("datetime")
                .HasColumnName("s_registerdate");
            entity.Property(e => e.SSportId).HasColumnName("s_sport_id");
            entity.Property(e => e.STime).HasColumnName("s_time");
            entity.Property(e => e.SType)
                .HasMaxLength(50)
                .HasColumnName("s_type");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TTeacherId).HasName("PK_eacher");

            entity.ToTable("Teacher");

            entity.Property(e => e.TTeacherId).HasColumnName("t_teacher_id");
            entity.Property(e => e.TApplytime)
                .HasColumnType("datetime")
                .HasColumnName("t_applytime");
            entity.Property(e => e.TCommission).HasColumnName("t_commission");
            entity.Property(e => e.TIntro)
                .HasMaxLength(200)
                .HasColumnName("t_intro");
            entity.Property(e => e.TLevel).HasColumnName("t_level");
            entity.Property(e => e.TMajor)
                .HasMaxLength(50)
                .HasColumnName("t_major");
            entity.Property(e => e.TMemberId).HasColumnName("t_member_id");
            entity.Property(e => e.TPrice).HasColumnName("t_price");
        });

        modelBuilder.Entity<Water>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.WDate)
                .HasColumnType("date")
                .HasColumnName("w_date");
            entity.Property(e => e.WDrinkId).HasColumnName("w_drink_id");
            entity.Property(e => e.WPlanId).HasColumnName("w_plan_id");
            entity.Property(e => e.WWaterId).HasColumnName("w_water_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
