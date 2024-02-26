﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YpassDesktop.DataAccess;

#nullable disable

namespace YpassDesktop.Migrations
{
    [DbContext(typeof(YpassDbContext))]
    partial class YpassDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

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
                    b.Property<int>("ManagerAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HashPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("IV")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("SaltCritical")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("ManagerAccountId");

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
