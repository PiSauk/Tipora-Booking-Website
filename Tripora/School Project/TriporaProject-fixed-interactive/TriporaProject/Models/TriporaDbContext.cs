using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TriporaProject.Models;

public partial class TriporaDbContext : DbContext
{
    public TriporaDbContext()
    {
    }

    public TriporaDbContext(DbContextOptions<TriporaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingSeat> BookingSeats { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleSeat> ScheduleSeats { get; set; }

    public virtual DbSet<SeatLayout> SeatLayouts { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BLACKPARADE\\SQLEXPRESS;Database=Tripora;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("booking_date");
            entity.Property(e => e.NumSeats).HasColumnName("num_seats");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Schedule");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_User");
        });

        modelBuilder.Entity<BookingSeat>(entity =>
        {
            entity.HasKey(e => e.BookingSeatId).HasName("PK__BookingS__FA4B94466C8CB4ED");

            entity.ToTable("BookingSeat");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SeatCode).HasMaxLength(10);

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingSeats)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingSeat_Booking");

            entity.HasOne(d => d.Schedule).WithMany(p => p.BookingSeats)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingSeat_Schedule");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.ToTable("Operator");

            entity.Property(e => e.OperatorId).HasColumnName("operator_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.OperatorName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("operator_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.PaymentDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Booking");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.ArrivalTime)
                .HasPrecision(0)
                .HasColumnName("arrival_time");
            entity.Property(e => e.AvailableSeats).HasColumnName("available_seats");
            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base_price");
            entity.Property(e => e.DepartureTime)
                .HasPrecision(0)
                .HasColumnName("departure_time");
            entity.Property(e => e.RouteId).HasColumnName("route_id");
            entity.Property(e => e.TransportId).HasColumnName("transport_id");

            entity.HasOne(d => d.Transport).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Transport");
        });

        modelBuilder.Entity<ScheduleSeat>(entity =>
        {
            entity.HasKey(e => e.ScheduleseatId).HasName("PK__Schedule__20EB989C3FBA6340");

            entity.ToTable("ScheduleSeat");

            entity.HasIndex(e => new { e.ScheduleId, e.SeatCode }, "UQ_ScheduleSeat").IsUnique();

            entity.Property(e => e.ScheduleseatId).HasColumnName("scheduleseat_id");
            entity.Property(e => e.HeldByUserId).HasColumnName("held_by_user_id");
            entity.Property(e => e.HoldExpiry)
                .HasColumnType("datetime")
                .HasColumnName("hold_expiry");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.SeatCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("seat_code");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("Available")
                .HasColumnName("status");

            entity.HasOne(d => d.HeldByUser).WithMany(p => p.ScheduleSeats)
                .HasForeignKey(d => d.HeldByUserId)
                .HasConstraintName("FK_ScheduleSeat_User");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleSeats)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleSeat_Schedule");
        });

        modelBuilder.Entity<SeatLayout>(entity =>
        {
            entity.HasKey(e => e.SeatlayoutId).HasName("PK__SeatLayo__075D3B49ADC40BB4");

            entity.ToTable("SeatLayout");

            entity.HasIndex(e => new { e.TransportId, e.SeatCode }, "UQ_SeatLayout").IsUnique();

            entity.Property(e => e.SeatlayoutId).HasColumnName("seatlayout_id");
            entity.Property(e => e.ColNo).HasColumnName("col_no");
            entity.Property(e => e.RowNo).HasColumnName("row_no");
            entity.Property(e => e.SeatCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("seat_code");
            entity.Property(e => e.TransportId).HasColumnName("transport_id");

            entity.HasOne(d => d.Transport).WithMany(p => p.SeatLayouts)
                .HasForeignKey(d => d.TransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeatLayout_Transport");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.ToTable("Transport");

            entity.HasIndex(e => e.BusNumber, "UQ_Transport_BusNumber").IsUnique();

            entity.Property(e => e.TransportId).HasColumnName("transport_id");
            entity.Property(e => e.BusNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bus_number");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.OperatorId).HasColumnName("operator_id");
            entity.Property(e => e.TransportType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transport_type");

            entity.HasOne(d => d.Operator).WithMany(p => p.Transports)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transport_Operator");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.HasIndex(e => e.Email, "UQ_User_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
