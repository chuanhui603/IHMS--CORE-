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

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<HealthInfo> HealthInfos { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MessageBoard> MessageBoards { get; set; }

    public virtual DbSet<MessageBoardDetail> MessageBoardDetails { get; set; }

    public virtual DbSet<MessageBoardImage> MessageBoardImages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<PointRecord> PointRecords { get; set; }

    public virtual DbSet<Questionset> Questionsets { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<SportDetail> SportDetails { get; set; }

    public virtual DbSet<Water> Water { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IHMS;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncemetId);

            entity.ToTable("Announcement");

            entity.Property(e => e.AnnouncemetId).HasColumnName("announcemet_id");
            entity.Property(e => e.Contents)
                .HasMaxLength(1500)
                .HasColumnName("contents");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
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

            entity.HasOne(d => d.Questionset).WithMany()
                .HasForeignKey(d => d.QuestionsetId)
                .HasConstraintName("FK_Answer_Questionset");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Member).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Members");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Schedule");
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

            entity.HasOne(d => d.Member).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coach_Members1");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.Coursename)
                .HasMaxLength(50)
                .HasColumnName("coursename");
            entity.Property(e => e.Image1)
                .HasMaxLength(50)
                .HasColumnName("image1");
            entity.Property(e => e.Image2)
                .HasMaxLength(50)
                .HasColumnName("image2");
            entity.Property(e => e.Image3)
                .HasMaxLength(50)
                .HasColumnName("image3");
            entity.Property(e => e.Intro)
                .HasMaxLength(100)
                .HasColumnName("intro");
            entity.Property(e => e.Video)
                .HasMaxLength(100)
                .HasColumnName("video");

            entity.HasOne(d => d.Coach).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CoachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Coach");
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
            entity.Property(e => e.Dname)
                .HasMaxLength(50)
                .HasColumnName("dname");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("img");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Diet).WithMany(p => p.DietDetails)
                .HasForeignKey(d => d.DietId)
                .HasConstraintName("FK_DietDetail_Diet");
        });

        modelBuilder.Entity<HealthInfo>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__HealthIn__B29B8534D9DD96E9");

            entity.ToTable("HealthInfo");

            entity.Property(e => e.MemberId)
                .ValueGeneratedOnAdd()
                .HasColumnName("member_id");
            entity.Property(e => e.BloodPressure)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("blood_pressure");
            entity.Property(e => e.BodyFatPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("body_fat_percentage");
            entity.Property(e => e.DateEntered)
                .HasColumnType("datetime")
                .HasColumnName("date_entered");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("height");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Member).WithOne(p => p.HealthInfo)
                .HasForeignKey<HealthInfo>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HealthInfo_Members");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__B29B85349B599CC2");

            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.AllergyDescription)
                .IsUnicode(false)
                .HasColumnName("allergy_description");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("avatar_image");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.DiseaseDescription)
                .IsUnicode(false)
                .HasColumnName("disease_description");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LoginTime)
                .HasColumnType("datetime")
                .HasColumnName("login_time");
            entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nickname");
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("occupation");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Permission).HasColumnName("permission");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ResidentialCity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("residential_city");
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

            entity.HasOne(d => d.Member).WithMany(p => p.MessageBoards)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message board_Members");
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

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message board details_Members");

            entity.HasOne(d => d.MemberNavigation).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message board details_message board");
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

            entity.HasOne(d => d.Message).WithMany()
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message board image_message board");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.Contents)
                .HasMaxLength(1000)
                .HasColumnName("contents");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Member).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_Members");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Createtime)
                .HasColumnType("datetime")
                .HasColumnName("createtime");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Ordernumber)
                .HasMaxLength(50)
                .HasColumnName("ordernumber");
            entity.Property(e => e.Pointstotal).HasColumnName("pointstotal");
            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .HasColumnName("reason");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Updatetime)
                .HasColumnType("datetime")
                .HasColumnName("updatetime");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Members");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderdetailId).HasColumnName("orderdetail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.Schedule).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Schedule");
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
        });

        modelBuilder.Entity<PointRecord>(entity =>
        {
            entity.ToTable("PointRecord");

            entity.Property(e => e.PointrecordId).HasColumnName("pointrecord_id");
            entity.Property(e => e.BankNumber).HasColumnName("bank_number");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Createtime)
                .HasColumnType("datetime")
                .HasColumnName("createtime");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Updatetime)
                .HasColumnType("datetime")
                .HasColumnName("updatetime");

            entity.HasOne(d => d.Member).WithMany(p => p.PointRecords)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PointRecord_Members");
        });

        modelBuilder.Entity<Questionset>(entity =>
        {
            entity.ToTable("Questionset");

            entity.Property(e => e.QuestionsetId)
                .ValueGeneratedNever()
                .HasColumnName("questionset_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Question)
                .HasMaxLength(500)
                .HasColumnName("question");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.Month)
                .HasColumnType("date")
                .HasColumnName("month");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");

            entity.HasOne(d => d.Course).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Course");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.ToTable("Score");

            entity.Property(e => e.ScoreId).HasColumnName("score_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Score1).HasColumnName("score");
            entity.Property(e => e.ScoredDate)
                .HasColumnType("datetime")
                .HasColumnName("scored_date");

            entity.HasOne(d => d.Course).WithMany(p => p.Scores)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_Course");

            entity.HasOne(d => d.Member).WithMany(p => p.Scores)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Score_Members");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.ToTable("Sport");

            entity.Property(e => e.SportId)
                .ValueGeneratedOnAdd()
                .HasColumnName("sport_id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Registerdate)
                .HasColumnType("datetime")
                .HasColumnName("registerdate");

            entity.HasOne(d => d.SportNavigation).WithOne(p => p.Sport)
                .HasForeignKey<Sport>(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sport_Plan1");
        });

        modelBuilder.Entity<SportDetail>(entity =>
        {
            entity.ToTable("Sport_Detail");

            entity.Property(e => e.SportDetailId)
                .ValueGeneratedNever()
                .HasColumnName("sport_detail_id");
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
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.Sporttime).HasColumnName("sporttime");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Sport).WithMany(p => p.SportDetails)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sport_Detail_Sport");
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
