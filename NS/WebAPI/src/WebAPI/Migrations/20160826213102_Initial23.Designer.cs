using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebAPI.Models;

namespace WebAPI.Migrations
{
    [DbContext(typeof(DSTUContext))]
    [Migration("20160826213102_Initial23")]
    partial class Initial23
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FacultyId");

                    b.Property<string>("Name");

                    b.Property<int?>("StudentId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("WebAPI.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("StudentId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("WebAPI.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Name");

                    b.Property<int?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("WebAPI.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookNumber");

                    b.Property<int>("Courseumber");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WebAPI.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Patronymic");

                    b.Property<string>("Position");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("WebAPI.Models.Department", b =>
                {
                    b.HasOne("WebAPI.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId");

                    b.HasOne("WebAPI.Models.Student")
                        .WithMany("Departments")
                        .HasForeignKey("StudentId");

                    b.HasOne("WebAPI.Models.Teacher")
                        .WithMany("Department")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("WebAPI.Models.Faculty", b =>
                {
                    b.HasOne("WebAPI.Models.Student")
                        .WithMany("Faculties")
                        .HasForeignKey("StudentId");

                    b.HasOne("WebAPI.Models.Teacher")
                        .WithMany("Faculties")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("WebAPI.Models.Group", b =>
                {
                    b.HasOne("WebAPI.Models.Department", "Department")
                        .WithMany("Groups")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("WebAPI.Models.Student")
                        .WithMany("Groups")
                        .HasForeignKey("StudentId");
                });
        }
    }
}
