﻿// <auto-generated />
using Extract_Data_From_PDF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Extract_Data_From_PDF.Migrations
{
    [DbContext(typeof(ContextDB))]
    partial class ContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Extract_Data_From_PDF.Models.ItRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorizationDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorizedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentRequested")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReasonForRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requestor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ItRequests");
                });

            modelBuilder.Entity("Extract_Data_From_PDF.Models.PdfData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Field1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Field2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PdfDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
