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

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseOrder> CourseOrders { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<HealthInfo> HealthInfos { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MessageBoard> MessageBoards { get; set; }

    public virtual DbSet<MessageBoardDetail> MessageBoardDetails { get; set; }

    public virtual DbSet<MessageBoardImage> MessageBoardImages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<PointRecord> PointRecords { get; set; }

    public virtual DbSet<Questionset> Questionsets { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<SportDetail> SportDetails { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Water> Water { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IHMS;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AMemberId).HasName("PK__Allergie__FE054288CC83F0BB");

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

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnAnnouncemetId);

            entity.ToTable("Announcement");

            entity.Property(e => e.AnAnnouncemetId).HasColumnName("an_announcemet_id");
            entity.Property(e => e.AnContent)
                .HasMaxLength(1500)
                .HasColumnName("an_content");
            entity.Property(e => e.AnImage)
                .HasMaxLength(50)
                .HasColumnName("an_image");
            entity.Property(e => e.AnTime)
                .HasColumnType("datetime")
                .HasColumnName("an_time");
            entity.Property(e => e.AnTitle)
                .HasMaxLength(100)
                .HasColumnName("an_title");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Answer");

            entity.Property(e => e.Answer1)
                .HasMaxLength(500)
                .HasColumnName("answer");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.QuestionsetId).HasColumnName("questionset_id");
            entity.Property(e => e.Time)
                .HasColumnType("date")
                .HasColumnName("time");
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

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.ToTable("Coach");

            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.Applytime)
                .HasColumnType("datetime")
                .HasColumnName("applytime");
            entity.Property(e => e.Commission).HasColumnName("commission");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.Confirmtime)
                .HasColumnType("datetime")
                .HasColumnName("confirmtime");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.Intro)
                .HasMaxLength(200)
                .HasColumnName("intro");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("reason");
            entity.Property(e => e.Resume)
                .HasMaxLength(50)
                .HasColumnName("resume");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Video)
                .HasMaxLength(100)
                .HasColumnName("video");
        });

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
            entity.ToTable("Diet");

            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Registerdate)
                .HasColumnType("datetime")
                .HasColumnName("registerdate");

            entity.HasOne(d => d.Plan).WithMany(p => p.Diets)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diet_Plan");
        });

        modelBuilder.Entity<DietDetail>(entity =>
        {
            entity.ToTable("DietDetail");

            entity.Property(e => e.DietDetailId).HasColumnName("diet_detail_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Decription)
                .HasMaxLength(100)
                .HasColumnName("decription");
            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("fname");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Diet).WithMany(p => p.DietDetails)
                .HasForeignKey(d => d.DietId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DietDetail_Diet");
        });

        modelBuilder.Entity<HealthInfo>(entity =>
        {
            entity.HasKey(e => e.HMemberId).HasName("PK__HealthIn__D1BAB1FCAF842794");

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
            entity.HasKey(e => e.MhMemberId).HasName("PK__MedicalH__35599D542D54605A");

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

            entity.HasIndex(e => e.MAccount, "UQ__Members__80FC9F79456ED206").IsUnique();

            entity.HasIndex(e => e.MAccount, "UQ__Members__80FC9F79E26F87D2").IsUnique();

            entity.HasIndex(e => e.MEmail, "UQ__Members__D12C572A4E894587").IsUnique();

            entity.HasIndex(e => e.MEmail, "UQ__Members__D12C572AA77AE704").IsUnique();

            entity.Property(e => e.MMemberId).HasColumnName("m_member_id");
            entity.Property(e => e.MAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("m_account");
            entity.Property(e => e.MAllergyDescription)
                .HasMaxLength(300)
                .HasColumnName("m_allergy_description");
            entity.Property(e => e.MAvatarImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("m_avatar_image");
            entity.Property(e => e.MBirthday)
                .HasColumnType("date")
                .HasColumnName("m_birthday");
            entity.Property(e => e.MDiseaseDescription)
                .HasMaxLength(300)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("m_phone");
            entity.Property(e => e.MResidentialCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("m_residential_city");
        });

        modelBuilder.Entity<MessageBoard>(entity =>
        {
            entity.HasKey(e => e.MessageId);

            entity.ToTable("message board");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Contents)
                .HasMaxLength(500)
                .HasColumnName("contents");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<MessageBoardDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("message board details");

            entity.Property(e => e.Contents).HasMaxLength(200);
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.MessageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("message_id");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
        });

        modelBuilder.Entity<MessageBoardImage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("message board image");

            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
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
            entity.HasKey(e => e.PlanId).HasName("PK_plan");

            entity.ToTable("Plan");

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.BodyPercentage).HasColumnName("body_percentage");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Pname)
                .HasMaxLength(50)
                .HasColumnName("pname");
            entity.Property(e => e.RegisterDate)
                .HasColumnType("datetime")
                .HasColumnName("register_date");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Member).WithMany(p => p.Plans)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_Members");
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

        modelBuilder.Entity<Questionset>(entity =>
        {
            entity.HasKey(e => e.QQuestionsetId);

            entity.ToTable("Questionset");

            entity.Property(e => e.QQuestionsetId).HasColumnName("q_questionset_id");
            entity.Property(e => e.QCategory)
                .HasMaxLength(50)
                .HasColumnName("q_category");
            entity.Property(e => e.QQuestion)
                .HasMaxLength(500)
                .HasColumnName("q_question");
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

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.ToTable("Sport");

            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Registerdate)
                .HasColumnType("datetime")
                .HasColumnName("registerdate");

            entity.HasOne(d => d.Plan).WithMany(p => p.Sports)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sport_Plan");
        });

        modelBuilder.Entity<SportDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sport_Detail");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.Sname)
                .HasMaxLength(50)
                .HasColumnName("sname");
            entity.Property(e => e.SportDetailId).HasColumnName("sport_detail_id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.Sporttime).HasColumnName("sporttime");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
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
            entity.Property(e => e.TReason)
                .HasMaxLength(100)
                .HasColumnName("t_reason");
            entity.Property(e => e.TResume)
                .HasMaxLength(100)
                .HasColumnName("t_resume");
            entity.Property(e => e.TScore).HasColumnName("t_score");
            entity.Property(e => e.TVideo)
                .HasMaxLength(100)
                .HasColumnName("t_video");
        });

        modelBuilder.Entity<Water>(entity =>
        {
            entity.Property(e => e.WaterId).HasColumnName("water_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Drink).HasColumnName("drink");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");

            entity.HasOne(d => d.Plan).WithMany(p => p.Water)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Water_Plan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
