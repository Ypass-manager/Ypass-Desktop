﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YpassDesktop.DataAccess;

#nullable disable

namespace YpassDesktop.Migrations
{
    [DbContext(typeof(YpassDbContext))]
    [Migration("20240117132715_InitiateTable")]
    partial class InitiateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Account", b =>
                {
                    b.Property<int>("accountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isFavorite")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("lastModification")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("websiteName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("websiteUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("accountId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("HistoryConnection", b =>
                {
                    b.Property<int>("historyConnectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("connectionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("historyConnectionId");

                    b.ToTable("HistoryConnection");
                });

            modelBuilder.Entity("ManagerAccount", b =>
                {
                    b.Property<int>("managerAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("accountName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("databaseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("hashPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("saltCrypt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("managerAccountId");

                    b.ToTable("ManagerAccount");
                });

            modelBuilder.Entity("Setting", b =>
                {
                    b.Property<int>("settingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("policiesChangePassDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("settingId");

                    b.ToTable("Setting");
                });
#pragma warning restore 612, 618
        }
    }
}