﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Zafaran.Charity;
using Zafaran.Charity.Models;

namespace Zafaran.Charity.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180530083903_sofreh_price")]
    partial class sofreh_price
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zafaran.Charity.Models.Charity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("PicturePath");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Charities");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharityId");

                    b.Property<string>("PaymentId");

                    b.Property<int>("PaymentStatus");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("StaticPhoneNumber");

                    b.Property<string>("UserIdentifier");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("CharityId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Price");

                    b.Property<int>("SofrehPrice");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.Supporter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Descriotion");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Size");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Supporters");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.TheNews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("PicturePath");

                    b.Property<int>("Size");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Zafaran.Charity.Models.Order", b =>
                {
                    b.HasOne("Zafaran.Charity.Models.Charity", "Charity")
                        .WithMany("Orders")
                        .HasForeignKey("CharityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zafaran.Charity.Models.OrderItem", b =>
                {
                    b.HasOne("Zafaran.Charity.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zafaran.Charity.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}