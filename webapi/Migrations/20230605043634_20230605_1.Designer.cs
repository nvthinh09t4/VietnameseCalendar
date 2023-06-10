﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Entities;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230605043634_20230605_1")]
    partial class _20230605_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("webapi.Entities.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Day")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayOff")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("HolidayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLunarHoliday")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReferenceLinks")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });
#pragma warning restore 612, 618
        }
    }
}
