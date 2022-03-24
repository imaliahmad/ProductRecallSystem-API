﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductRecallSystem.DAL.Data;

namespace ProductRecallSystem.DAL.Migrations
{
    [DbContext(typeof(EFCodeDbContext))]
    [Migration("20220321163828_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductRecallSystem.BOL.Announcements", b =>
                {
                    b.Property<long>("AnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RecallID")
                        .HasColumnType("bigint");

                    b.HasKey("AnnouncementId");

                    b.HasIndex("RecallID");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Manufacturers", b =>
                {
                    b.Property<long>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Products", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ManufacturerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Recalls", b =>
                {
                    b.Property<long>("RecallID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RecallID");

                    b.HasIndex("ProductId");

                    b.ToTable("Recalls");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Announcements", b =>
                {
                    b.HasOne("ProductRecallSystem.BOL.Recalls", "Recalls")
                        .WithMany("Announcements")
                        .HasForeignKey("RecallID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recalls");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Products", b =>
                {
                    b.HasOne("ProductRecallSystem.BOL.Manufacturers", "Manufacturers")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturers");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Recalls", b =>
                {
                    b.HasOne("ProductRecallSystem.BOL.Products", "Products")
                        .WithMany("Recalls")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Manufacturers", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Products", b =>
                {
                    b.Navigation("Recalls");
                });

            modelBuilder.Entity("ProductRecallSystem.BOL.Recalls", b =>
                {
                    b.Navigation("Announcements");
                });
#pragma warning restore 612, 618
        }
    }
}