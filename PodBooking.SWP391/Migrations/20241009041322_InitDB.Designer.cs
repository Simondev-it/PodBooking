﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodBooking.SWP391.Models;

#nullable disable

namespace PodBooking.SWP391.Migrations
{
    [DbContext(typeof(Swp391Context))]
    [Migration("20241009041322_InitDB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PodBooking.SWP391.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PodId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Booking__3214EC07033F6074");

                    b.HasIndex("PodId");

                    b.HasIndex("UserId");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.BookingOrder", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__BookingO__3214EC074182E108");

                    b.HasIndex("BookingId");

                    b.HasIndex("ProductId");

                    b.ToTable("BookingOrder", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Category__3214EC07081FA9CF");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Payment__3214EC07EC1C4642");

                    b.HasIndex("BookingId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Pod", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Pod__3214EC072441E54F");

                    b.HasIndex("StoreId");

                    b.HasIndex("TypeId");

                    b.ToTable("Pod", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Product__3214EC07E9AD558B");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StoreId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Slot", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("EndTime")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PodId")
                        .HasColumnType("int");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("StartTime")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Slot__3214EC07CE8BCA51");

                    b.HasIndex("PodId");

                    b.ToTable("Slot", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Contact")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Store__3214EC075F7187A7");

                    b.ToTable("Store", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Type__3214EC072196B86E");

                    b.ToTable("Type", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Point")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__User__3214EC07DC1CEA78");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Utility", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Utility__3214EC07BD2A23F7");

                    b.ToTable("Utility", (string)null);
                });

            modelBuilder.Entity("SlotBooking", b =>
                {
                    b.Property<int>("SlotId")
                        .HasColumnType("int");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.HasKey("SlotId", "BookingId")
                        .HasName("PK__SlotBook__CD2B1B01226B634F");

                    b.HasIndex("BookingId");

                    b.ToTable("SlotBooking", (string)null);
                });

            modelBuilder.Entity("UtilityPod", b =>
                {
                    b.Property<int>("UtilityId")
                        .HasColumnType("int");

                    b.Property<int>("PodId")
                        .HasColumnType("int");

                    b.HasKey("UtilityId", "PodId")
                        .HasName("PK__UtilityP__7DE1BEB0AC193726");

                    b.HasIndex("PodId");

                    b.ToTable("UtilityPod", (string)null);
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Booking", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Pod", "Pod")
                        .WithMany("Bookings")
                        .HasForeignKey("PodId")
                        .IsRequired()
                        .HasConstraintName("FkBookingPod");

                    b.HasOne("PodBooking.SWP391.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FkBookingUser");

                    b.Navigation("Pod");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.BookingOrder", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Booking", "Booking")
                        .WithMany("BookingOrders")
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FkBookingOrderBooking");

                    b.HasOne("PodBooking.SWP391.Models.Product", "Product")
                        .WithMany("BookingOrders")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FkBookingOrderProduct");

                    b.Navigation("Booking");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Payment", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Booking", "Booking")
                        .WithMany("Payments")
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FkPaymentBooking");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Pod", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Store", "Store")
                        .WithMany("Pods")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FkPodStore");

                    b.HasOne("PodBooking.SWP391.Models.Type", "Type")
                        .WithMany("Pods")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("FkPodType");

                    b.Navigation("Store");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Product", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FkProductCategory");

                    b.HasOne("PodBooking.SWP391.Models.Store", "Store")
                        .WithMany("Products")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FkProductStore");

                    b.Navigation("Category");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Slot", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Pod", "Pod")
                        .WithMany("Slots")
                        .HasForeignKey("PodId")
                        .IsRequired()
                        .HasConstraintName("FkSlotPod");

                    b.Navigation("Pod");
                });

            modelBuilder.Entity("SlotBooking", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Booking", null)
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .IsRequired()
                        .HasConstraintName("FkBooking");

                    b.HasOne("PodBooking.SWP391.Models.Slot", null)
                        .WithMany()
                        .HasForeignKey("SlotId")
                        .IsRequired()
                        .HasConstraintName("FkSlot");
                });

            modelBuilder.Entity("UtilityPod", b =>
                {
                    b.HasOne("PodBooking.SWP391.Models.Pod", null)
                        .WithMany()
                        .HasForeignKey("PodId")
                        .IsRequired()
                        .HasConstraintName("FkPod");

                    b.HasOne("PodBooking.SWP391.Models.Utility", null)
                        .WithMany()
                        .HasForeignKey("UtilityId")
                        .IsRequired()
                        .HasConstraintName("FkUtility");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Booking", b =>
                {
                    b.Navigation("BookingOrders");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Pod", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Product", b =>
                {
                    b.Navigation("BookingOrders");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Store", b =>
                {
                    b.Navigation("Pods");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.Type", b =>
                {
                    b.Navigation("Pods");
                });

            modelBuilder.Entity("PodBooking.SWP391.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}