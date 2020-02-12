using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MeetSport.Models
{
    public partial class projetContext : DbContext
    {
        public IConfiguration Configuration { get; }
        
        public projetContext()
        {
        }

        public projetContext(IConfiguration configuration, DbContextOptions<projetContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<AthleticUser> AthleticUser { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventAttendee> EventAttendee { get; set; }
        public virtual DbSet<EventChat> EventChat { get; set; }
        public virtual DbSet<EventComment> EventComment { get; set; }
        public virtual DbSet<MobileUser> MobileUser { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleClaim> RoleClaim { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(Configuration.GetConnectionString("MeetSportUrl"), x => x.ServerVersion(Configuration.GetConnectionString("MeetSportVersion")));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AthleticUser>(entity =>
            {
                entity.ToTable("ATHLETIC_USER");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(19,16)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(19,16)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("decimal(3,1)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AthleticUser)
                    .HasForeignKey<AthleticUser>(d => d.Id)
                    .HasConstraintName("ATHLETIC_USER_fk_id");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("CLAIM");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.HasIndex(e => e.Creator)
                    .HasName("EVENT_key_creator");

                entity.HasIndex(e => e.Place)
                    .HasName("EVENT_key_place");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dislikes)
                    .HasColumnName("dislikes")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Place)
                    .HasColumnName("place")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.Creator)
                    .HasConstraintName("EVENT_fk_creator");

                entity.HasOne(d => d.PlaceNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.Place)
                    .HasConstraintName("EVENT_fk_place");
            });

            modelBuilder.Entity<EventAttendee>(entity =>
            {
                entity.HasKey(e => new { e.Event, e.User })
                    .HasName("PRIMARY");

                entity.ToTable("EVENT_ATTENDEE");

                entity.HasIndex(e => e.Event)
                    .HasName("EVENT_ATTENDEE_key_event");

                entity.HasIndex(e => e.User)
                    .HasName("EVENT_ATTENDEE_key_user");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.EventAttendee)
                    .HasForeignKey(d => d.Event)
                    .HasConstraintName("EVENT_ATTENDEE_fk_event");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.EventAttendee)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("EVENT_ATTENDEE_fk_user");
            });

            modelBuilder.Entity<EventChat>(entity =>
            {
                entity.ToTable("EVENT_CHAT");

                entity.HasIndex(e => e.Event)
                    .HasName("EVENT_CHAT_key_event");

                entity.HasIndex(e => e.User)
                    .HasName("EVENT_CHAT_key_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.EventChat)
                    .HasForeignKey(d => d.Event)
                    .HasConstraintName("EVENT_CHAT_fk_event");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.EventChat)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("EVENT_CHAT_fk_user");
            });

            modelBuilder.Entity<EventComment>(entity =>
            {
                entity.ToTable("EVENT_COMMENT");

                entity.HasIndex(e => e.Event)
                    .HasName("EVENT_COMMENT_key_event");

                entity.HasIndex(e => e.User)
                    .HasName("EVENT_COMMENT_key_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.EventComment)
                    .HasForeignKey(d => d.Event)
                    .HasConstraintName("EVENT_COMMENT_fk_event");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.EventComment)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("EVENT_COMMENT_fk_user");
            });

            modelBuilder.Entity<MobileUser>(entity =>
            {
                entity.ToTable("MOBILE_USER");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.FirebaseToken)
                    .HasColumnName("firebase_token")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.MobileUser)
                    .HasForeignKey<MobileUser>(d => d.Id)
                    .HasConstraintName("MOBILE_USER_fk_id");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("PLACE");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Adress)
                    .IsRequired()
                    .HasColumnName("adress")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(19,16)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(19,16)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.HasKey(e => new { e.Role, e.Claim })
                    .HasName("PRIMARY");

                entity.ToTable("ROLE_CLAIM");

                entity.HasIndex(e => e.Claim)
                    .HasName("ROLE_CLAIM_key_claim");

                entity.HasIndex(e => e.Role)
                    .HasName("ROLE_CLAIM_key_role");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Claim)
                    .HasColumnName("claim")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.ClaimNavigation)
                    .WithMany(p => p.RoleClaim)
                    .HasForeignKey(d => d.Claim)
                    .HasConstraintName("ROLE_CLAIM_fk_claim");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.RoleClaim)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("ROLE_CLAIM_fk_role");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.Mail)
                    .HasName("USER_key_mail")
                    .IsUnique();

                entity.HasIndex(e => e.Role)
                    .HasName("USER_key_role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasColumnName("hashed_password")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasColumnType("varchar(320)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("USER_fk_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
