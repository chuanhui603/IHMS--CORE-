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

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleImage> ArticleImages { get; set; }

    public virtual DbSet<AvailableTime> AvailableTimes { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<CoachAvailableTime> CoachAvailableTimes { get; set; }

    public virtual DbSet<CoachContact> CoachContacts { get; set; }

    public virtual DbSet<CoachExperience> CoachExperiences { get; set; }

    public virtual DbSet<CoachLicense> CoachLicenses { get; set; }

    public virtual DbSet<CoachRate> CoachRates { get; set; }

    public virtual DbSet<CoachSkill> CoachSkills { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CustomerService> CustomerServices { get; set; }

    public virtual DbSet<Diet> Diets { get; set; }

    public virtual DbSet<DietDetail> DietDetails { get; set; }

    public virtual DbSet<DietImg> DietImgs { get; set; }

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

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<SportDetail> SportDetails { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

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

        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("article");

            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.Contents)
                .HasMaxLength(1500)
                .HasColumnName("contents");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ArticleImage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("article_image");

            entity.Property(e => e.ArticleId).HasColumnName("article_id");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasColumnName("image");
        });

        modelBuilder.Entity<AvailableTime>(entity =>
        {
            entity.Property(e => e.AvailableTimeId).HasColumnName("AvailableTimeID");
            entity.Property(e => e.AvailableTime1)
                .HasMaxLength(50)
                .HasColumnName("AvailableTime");
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
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(50);
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.ToTable("Coach");

            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.ApplyDate).HasMaxLength(50);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CoachImage).HasMaxLength(50);
            entity.Property(e => e.CoachName).HasMaxLength(50);
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.City).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Coach_Cities1");

            entity.HasOne(d => d.Member).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Coach_Members");
        });

        modelBuilder.Entity<CoachAvailableTime>(entity =>
        {
            entity.HasKey(e => e.CoachTimeId);

            entity.ToTable("CoachAvailableTime");

            entity.Property(e => e.CoachTimeId).HasColumnName("CoachTimeID");
            entity.Property(e => e.AvailableTimeId).HasColumnName("AvailableTimeID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");

            entity.HasOne(d => d.AvailableTime).WithMany(p => p.CoachAvailableTimes)
                .HasForeignKey(d => d.AvailableTimeId)
                .HasConstraintName("FK_CoachAvailableTime_AvailableTimes");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachAvailableTimes)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachAvailableTime_Coach");
        });

        modelBuilder.Entity<CoachContact>(entity =>
        {
            entity.Property(e => e.CoachContactId).HasColumnName("CoachContactID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.ContactDate).HasMaxLength(50);
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachContacts)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachContacts_Coach");
        });

        modelBuilder.Entity<CoachExperience>(entity =>
        {
            entity.HasKey(e => e.ExperienceId).HasName("PK_Experience");

            entity.ToTable("CoachExperience");

            entity.Property(e => e.ExperienceId).HasColumnName("ExperienceID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachExperiences)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachExperience_Coach");
        });

        modelBuilder.Entity<CoachLicense>(entity =>
        {
            entity.HasKey(e => e.LicenseId);

            entity.ToTable("CoachLicense");

            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachLicenses)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachLicense_Coach");
        });

        modelBuilder.Entity<CoachRate>(entity =>
        {
            entity.HasKey(e => e.RateId);

            entity.Property(e => e.RateId).HasColumnName("RateID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.FRateDate)
                .HasMaxLength(50)
                .HasColumnName("fRateDate");
            entity.Property(e => e.FRateText).HasColumnName("fRateText");
            entity.Property(e => e.MemberId).HasColumnName("member_id");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachRates)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachRates_Coach");
        });

        modelBuilder.Entity<CoachSkill>(entity =>
        {
            entity.ToTable("CoachSkill");

            entity.Property(e => e.CoachSkillId).HasColumnName("CoachSkillID");
            entity.Property(e => e.CoachId).HasColumnName("coach_id");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");

            entity.HasOne(d => d.Coach).WithMany(p => p.CoachSkills)
                .HasForeignKey(d => d.CoachId)
                .HasConstraintName("FK_CoachSkill_Coach");

            entity.HasOne(d => d.Skill).WithMany(p => p.CoachSkills)
                .HasForeignKey(d => d.SkillId)
                .HasConstraintName("FK_CoachSkill_Skills");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CoachContactId).HasColumnName("CoachContactID");
            entity.Property(e => e.CourseName).HasMaxLength(50);

            entity.HasOne(d => d.CoachContact).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CoachContactId)
                .HasConstraintName("FK_Course_CoachContacts");
        });

        modelBuilder.Entity<CustomerService>(entity =>
        {
            entity.HasKey(e => e.CustomerServiceId).HasName("PK_custome service");

            entity.ToTable("CustomerService");

            entity.Property(e => e.CustomerServiceId).HasColumnName("customer_service_id");
            entity.Property(e => e.Contents)
                .HasMaxLength(1500)
                .HasColumnName("contents");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Reply)
                .HasMaxLength(1500)
                .HasColumnName("reply");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedTime)
                .HasColumnType("datetime")
                .HasColumnName("updated_time");

            entity.HasOne(d => d.Member).WithMany(p => p.CustomerServices)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_custome service_Members");
        });

        modelBuilder.Entity<Diet>(entity =>
        {
            entity.ToTable("Diet");

            entity.Property(e => e.DietId).HasColumnName("diet_id");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");

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
                .HasMaxLength(30)
                .HasColumnName("dname");
            entity.Property(e => e.Registerdate)
                .HasColumnType("datetime")
                .HasColumnName("registerdate");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.Diet).WithMany(p => p.DietDetails)
                .HasForeignKey(d => d.DietId)
                .HasConstraintName("FK_DietDetail_Diet");
        });

        modelBuilder.Entity<DietImg>(entity =>
        {
            entity.ToTable("Diet_Img");

            entity.Property(e => e.DietImgId).HasColumnName("diet_img_id");
            entity.Property(e => e.DietDetailId).HasColumnName("diet_detail_id");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .HasColumnName("img");

            entity.HasOne(d => d.DietDetail).WithMany(p => p.DietImgs)
                .HasForeignKey(d => d.DietDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diet_Img_DietDetail");
        });

        modelBuilder.Entity<HealthInfo>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__HealthIn__B29B8534957EE41F");

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
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Bmr).HasColumnName("bmr");
            entity.Property(e => e.BodyPercentage).HasColumnName("body_percentage");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.RegisterDate)
                .HasColumnType("datetime")
                .HasColumnName("register_date");
            entity.Property(e => e.Tdee).HasColumnName("tdee");
            entity.Property(e => e.Times)
                .HasMaxLength(20)
                .HasColumnName("times");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Member).WithMany(p => p.Plans)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_Members");
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
            entity.Property(e => e.CourseTime).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CourseId)
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

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.SkillDescription).HasMaxLength(50);
            entity.Property(e => e.SkillImage).HasMaxLength(50);
            entity.Property(e => e.SkillName).HasMaxLength(50);
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.ToTable("Sport");

            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");

            entity.HasOne(d => d.Plan).WithMany(p => p.Sports)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sport_Plan");
        });

        modelBuilder.Entity<SportDetail>(entity =>
        {
            entity.ToTable("Sport_Detail");

            entity.Property(e => e.SportDetailId).HasColumnName("sport_detail_id");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.Isdone).HasColumnName("isdone");
            entity.Property(e => e.Registerdate)
                .HasColumnType("datetime")
                .HasColumnName("registerdate");
            entity.Property(e => e.Sets).HasColumnName("sets");
            entity.Property(e => e.Sname)
                .HasMaxLength(50)
                .HasColumnName("sname");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.Sportdate)
                .HasColumnType("datetime")
                .HasColumnName("sportdate");
            entity.Property(e => e.Time)
                .HasMaxLength(10)
                .HasColumnName("time");
            entity.Property(e => e.Timelong).HasColumnName("timelong");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.Sport).WithMany(p => p.SportDetails)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sport_Detail_Sport");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Status1)
                .HasMaxLength(50)
                .HasColumnName("Status");
        });

        modelBuilder.Entity<Water>(entity =>
        {
            entity.Property(e => e.WaterId).HasColumnName("water_id");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
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
