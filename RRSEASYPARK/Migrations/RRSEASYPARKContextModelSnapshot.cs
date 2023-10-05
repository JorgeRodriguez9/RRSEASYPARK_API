﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RRSEASYPARK.DAL;

#nullable disable

namespace RRSEASYPARK.Migrations
{
    [DbContext(typeof(RRSEASYPARKContext))]
    partial class RRSEASYPARKContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RRSEasyPark.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("RRSEasyPark.Models.ClientParkingLot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<long>("Identification")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("Telephone")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ClientParkingLot");
                });

            modelBuilder.Entity("RRSEasyPark.Models.ParkingLot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adress")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("CantSpacesCar")
                        .HasColumnType("int");

                    b.Property<int>("CantSpacesDisability")
                        .HasColumnType("int");

                    b.Property<int>("CantSpacesMotorcycle")
                        .HasColumnType("int");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DisabilityPrice")
                        .HasColumnType("int");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nit")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("NormalPrice")
                        .HasColumnType("int");

                    b.Property<Guid>("PropietaryParkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Telephone")
                        .HasColumnType("bigint");

                    b.Property<string>("disabilityservices")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PropietaryParkId");

                    b.ToTable("parkingLots");
                });

            modelBuilder.Entity("RRSEasyPark.Models.PropietaryPark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<long>("Identification")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("propietaryParks");
                });

            modelBuilder.Entity("RRSEasyPark.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientParkingLotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Disabled")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParkingLotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.Property<Guid>("TypeVehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClientParkingLotId");

                    b.HasIndex("ParkingLotId");

                    b.HasIndex("TypeVehicleId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("RRSEasyPark.Models.Rol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("rols");
                });

            modelBuilder.Entity("RRSEasyPark.Models.TypeVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("typeVehicles");
                });

            modelBuilder.Entity("RRSEasyPark.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("RolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("RRSEasyPark.Models.Value", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ValueHC")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("values");
                });

            modelBuilder.Entity("RRSEasyPark.Models.ClientParkingLot", b =>
                {
                    b.HasOne("RRSEasyPark.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RRSEasyPark.Models.ParkingLot", b =>
                {
                    b.HasOne("RRSEasyPark.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RRSEasyPark.Models.PropietaryPark", "PropietaryPark")
                        .WithMany()
                        .HasForeignKey("PropietaryParkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("PropietaryPark");
                });

            modelBuilder.Entity("RRSEasyPark.Models.PropietaryPark", b =>
                {
                    b.HasOne("RRSEasyPark.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RRSEasyPark.Models.Reservation", b =>
                {
                    b.HasOne("RRSEasyPark.Models.ClientParkingLot", "ClientParkingLot")
                        .WithMany()
                        .HasForeignKey("ClientParkingLotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RRSEasyPark.Models.ParkingLot", "ParkingLot")
                        .WithMany()
                        .HasForeignKey("ParkingLotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RRSEasyPark.Models.TypeVehicle", "TypeVehicle")
                        .WithMany()
                        .HasForeignKey("TypeVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientParkingLot");

                    b.Navigation("ParkingLot");

                    b.Navigation("TypeVehicle");
                });

            modelBuilder.Entity("RRSEasyPark.Models.User", b =>
                {
                    b.HasOne("RRSEasyPark.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });
#pragma warning restore 612, 618
        }
    }
}
