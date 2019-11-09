﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project100.Models;

namespace Project100.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20191108202022_day52")]
    partial class day52
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project100.Models.Class.Business", b =>
                {
                    b.Property<int>("accountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomersId")
                        .HasColumnType("int");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("accountNumber");

                    b.HasIndex("CustomersId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("Project100.Models.Class.Checking", b =>
                {
                    b.Property<int>("accountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomersId")
                        .HasColumnType("int");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("accountNumber");

                    b.HasIndex("CustomersId");

                    b.ToTable("Checking");
                });

            modelBuilder.Entity("Project100.Models.Class.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("registerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Project100.Models.Loan", b =>
                {
                    b.Property<int>("accountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomersId")
                        .HasColumnType("int");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("accountNumber");

                    b.HasIndex("CustomersId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("Project100.Models.Term", b =>
                {
                    b.Property<int>("accountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomersId")
                        .HasColumnType("int");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("period")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("accountNumber");

                    b.HasIndex("CustomersId");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("Project100.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BusinessaccountNumber")
                        .HasColumnType("int");

                    b.Property<int?>("CheckingaccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId1")
                        .HasColumnType("int");

                    b.Property<int?>("LoanaccountNumber")
                        .HasColumnType("int");

                    b.Property<int?>("TermaccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("accountNumber")
                        .HasColumnType("int");

                    b.Property<string>("accountType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("amount")
                        .HasColumnType("float");

                    b.Property<double>("balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("numberOfMonth")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessaccountNumber");

                    b.HasIndex("CheckingaccountNumber");

                    b.HasIndex("CustomerId1");

                    b.HasIndex("LoanaccountNumber");

                    b.HasIndex("TermaccountNumber");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Project100.Models.Class.Business", b =>
                {
                    b.HasOne("Project100.Models.Class.Customers", "Customers")
                        .WithMany("Business")
                        .HasForeignKey("CustomersId");
                });

            modelBuilder.Entity("Project100.Models.Class.Checking", b =>
                {
                    b.HasOne("Project100.Models.Class.Customers", "Customers")
                        .WithMany("Checking")
                        .HasForeignKey("CustomersId");
                });

            modelBuilder.Entity("Project100.Models.Loan", b =>
                {
                    b.HasOne("Project100.Models.Class.Customers", "Customers")
                        .WithMany("Loan")
                        .HasForeignKey("CustomersId");
                });

            modelBuilder.Entity("Project100.Models.Term", b =>
                {
                    b.HasOne("Project100.Models.Class.Customers", "Customers")
                        .WithMany("Terms")
                        .HasForeignKey("CustomersId");
                });

            modelBuilder.Entity("Project100.Models.Transaction", b =>
                {
                    b.HasOne("Project100.Models.Class.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessaccountNumber");

                    b.HasOne("Project100.Models.Class.Checking", "Checking")
                        .WithMany()
                        .HasForeignKey("CheckingaccountNumber");

                    b.HasOne("Project100.Models.Class.Customers", "Customer")
                        .WithMany("Transaction")
                        .HasForeignKey("CustomerId1");

                    b.HasOne("Project100.Models.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanaccountNumber");

                    b.HasOne("Project100.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermaccountNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
