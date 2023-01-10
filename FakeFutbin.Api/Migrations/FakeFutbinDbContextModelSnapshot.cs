﻿// <auto-generated />
using System;
using FakeFutbin.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FakeFutbin.Api.Migrations
{
    [DbContext(typeof(FakeFutbinDbContext))]
    partial class FakeFutbinDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FakeFutbin.Api.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MarketValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("Raiting")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationalityId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 30,
                            ImageURL = "/Images/Brazil/Brazil1.jpg",
                            MarketValue = 30000,
                            Name = "Carlos Henrique Casimiro",
                            NationalityId = 1,
                            Position = "CDM,CM",
                            Qty = 15,
                            Raiting = 89
                        },
                        new
                        {
                            Id = 2,
                            Age = 25,
                            ImageURL = "/Images/Brazil/Brazil2.jpg",
                            MarketValue = 35000,
                            Name = "Gabriel Jesus",
                            NationalityId = 1,
                            Position = "ST,RW",
                            Qty = 20,
                            Raiting = 89
                        },
                        new
                        {
                            Id = 3,
                            Age = 34,
                            ImageURL = "/Images/Brazil/Brazil3.jpg",
                            MarketValue = 500000,
                            Name = "Marcelo Vieira da Silva Júnior",
                            NationalityId = 1,
                            Position = "LB,LM",
                            Qty = 5,
                            Raiting = 94
                        },
                        new
                        {
                            Id = 4,
                            Age = 30,
                            ImageURL = "/Images/Brazil/Brazil4.jpg",
                            MarketValue = 264000,
                            Name = "Neymar da Silva Santos Jr.",
                            NationalityId = 1,
                            Position = "LW,CAM",
                            Qty = 7,
                            Raiting = 96
                        },
                        new
                        {
                            Id = 5,
                            Age = 22,
                            ImageURL = "/Images/Brazil/Brazil5.jpg",
                            MarketValue = 221000,
                            Name = "Vinícius José de Oliveira Júnior",
                            NationalityId = 1,
                            Position = "LW,RW",
                            Qty = 13,
                            Raiting = 96
                        },
                        new
                        {
                            Id = 6,
                            Age = 42,
                            ImageURL = "/Images/England/England1.jpg",
                            MarketValue = 215000,
                            Name = "Steven Gerrard",
                            NationalityId = 2,
                            Position = "CM,CDM",
                            Qty = 4,
                            Raiting = 92
                        },
                        new
                        {
                            Id = 7,
                            Age = 44,
                            ImageURL = "/Images/England/England2.jpg",
                            MarketValue = 132000,
                            Name = "Frank Lampard",
                            NationalityId = 2,
                            Position = "CM,CAM",
                            Qty = 6,
                            Raiting = 91
                        },
                        new
                        {
                            Id = 8,
                            Age = 36,
                            ImageURL = "/Images/England/England3.jpg",
                            MarketValue = 349000,
                            Name = "Wayne Rooney",
                            NationalityId = 2,
                            Position = "ST,CF",
                            Qty = 8,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 9,
                            Age = 22,
                            ImageURL = "/Images/England/England4.jpg",
                            MarketValue = 110000,
                            Name = "Jadon Sancho",
                            NationalityId = 2,
                            Position = "RM,RW",
                            Qty = 20,
                            Raiting = 91
                        },
                        new
                        {
                            Id = 10,
                            Age = 47,
                            ImageURL = "/Images/England/England5.jpg",
                            MarketValue = 117000,
                            Name = "Paul Scholes",
                            NationalityId = 2,
                            Position = "CM,CAM",
                            Qty = 6,
                            Raiting = 92
                        },
                        new
                        {
                            Id = 11,
                            Age = 34,
                            ImageURL = "/Images/France/France1.jpg",
                            MarketValue = 230000,
                            Name = "Karim Benzema",
                            NationalityId = 3,
                            Position = "CF,ST",
                            Qty = 9,
                            Raiting = 98
                        },
                        new
                        {
                            Id = 12,
                            Age = 56,
                            ImageURL = "/Images/France/France2.jpg",
                            MarketValue = 390000,
                            Name = "Eric Cantona",
                            NationalityId = 3,
                            Position = "CF,ST",
                            Qty = 5,
                            Raiting = 94
                        },
                        new
                        {
                            Id = 13,
                            Age = 45,
                            ImageURL = "/Images/France/France3.jpg",
                            MarketValue = 562000,
                            Name = "Thierry Henry",
                            NationalityId = 3,
                            Position = "ST,LW",
                            Qty = 8,
                            Raiting = 94
                        },
                        new
                        {
                            Id = 14,
                            Age = 49,
                            ImageURL = "/Images/France/France4.jpg",
                            MarketValue = 110000,
                            Name = "Claude Makélélé",
                            NationalityId = 3,
                            Position = "CM,CDM",
                            Qty = 10,
                            Raiting = 91
                        },
                        new
                        {
                            Id = 15,
                            Age = 50,
                            ImageURL = "/Images/France/France5.jpg",
                            MarketValue = 1581000,
                            Name = "Zinedine Zidane",
                            NationalityId = 3,
                            Position = "CAM,CM",
                            Qty = 3,
                            Raiting = 97
                        },
                        new
                        {
                            Id = 16,
                            Age = 48,
                            ImageURL = "/Images/Italy/Italy1.jpg",
                            MarketValue = 195000,
                            Name = "Fabio Cannavaro",
                            NationalityId = 4,
                            Position = "CB,RB",
                            Qty = 6,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 17,
                            Age = 38,
                            ImageURL = "/Images/Italy/Italy2.jpg",
                            MarketValue = 137000,
                            Name = "Giorgio Chiellini",
                            NationalityId = 4,
                            Position = "CB,LB",
                            Qty = 11,
                            Raiting = 96
                        },
                        new
                        {
                            Id = 18,
                            Age = 47,
                            ImageURL = "/Images/Italy/Italy3.jpg",
                            MarketValue = 155000,
                            Name = "Alessandro Del Piero",
                            NationalityId = 4,
                            Position = "CF,ST",
                            Qty = 13,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 19,
                            Age = 44,
                            ImageURL = "/Images/Italy/Italy4.jpg",
                            MarketValue = 100000,
                            Name = "Gennaro Gattuso",
                            NationalityId = 4,
                            Position = "CDM,CM",
                            Qty = 15,
                            Raiting = 90
                        },
                        new
                        {
                            Id = 20,
                            Age = 43,
                            ImageURL = "/Images/Italy/Italy5.jpg",
                            MarketValue = 179000,
                            Name = "Andrea Pirlo",
                            NationalityId = 4,
                            Position = "CM,CAM",
                            Qty = 12,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 21,
                            Age = 41,
                            ImageURL = "/Images/Spain/Spain1.jpg",
                            MarketValue = 339000,
                            Name = "Iker Casillas",
                            NationalityId = 5,
                            Position = "GK",
                            Qty = 5,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 22,
                            Age = 38,
                            ImageURL = "/Images/Spain/Spain2.jpg",
                            MarketValue = 10000,
                            Name = "Andrés Iniesta Luján",
                            NationalityId = 5,
                            Position = "CM,CAM",
                            Qty = 13,
                            Raiting = 83
                        },
                        new
                        {
                            Id = 23,
                            Age = 36,
                            ImageURL = "/Images/Spain/Spain3.jpg",
                            MarketValue = 8500,
                            Name = "Sergio Ramos García",
                            NationalityId = 5,
                            Position = "CB,RB",
                            Qty = 17,
                            Raiting = 88
                        },
                        new
                        {
                            Id = 24,
                            Age = 45,
                            ImageURL = "/Images/Spain/Spain4.jpg",
                            MarketValue = 105000,
                            Name = "Raúl González Blanco",
                            NationalityId = 5,
                            Position = "CF,ST",
                            Qty = 5,
                            Raiting = 93
                        },
                        new
                        {
                            Id = 25,
                            Age = 38,
                            ImageURL = "/Images/Spain/Spain5.jpg",
                            MarketValue = 156000,
                            Name = "Fernando Torres",
                            NationalityId = 5,
                            Position = "ST,CF",
                            Qty = 9,
                            Raiting = 92
                        });
                });

            modelBuilder.Entity("FakeFutbin.Api.Entities.PlayerNationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlayerNationalities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageURL = "/Images/Nationalities/Nationality1.jpg",
                            Name = "Brazil"
                        },
                        new
                        {
                            Id = 2,
                            ImageURL = "/Images/Nationalities/Nationality2.jpg",
                            Name = "England"
                        },
                        new
                        {
                            Id = 3,
                            ImageURL = "/Images/Nationalities/Nationality3.jpg",
                            Name = "France"
                        },
                        new
                        {
                            Id = 4,
                            ImageURL = "/Images/Nationalities/Nationality4.jpg",
                            Name = "Italy"
                        },
                        new
                        {
                            Id = 5,
                            ImageURL = "/Images/Nationalities/Nationality5.jpg",
                            Name = "Spain"
                        });
                });

            modelBuilder.Entity("FakeFutbin.Api.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wallet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FakeFutbin.Api.Entities.UserPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserPlayers");
                });

            modelBuilder.Entity("FakeFutbin.Api.Entities.Player", b =>
                {
                    b.HasOne("FakeFutbin.Api.Entities.PlayerNationality", "PlayerNationality")
                        .WithMany()
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerNationality");
                });
#pragma warning restore 612, 618
        }
    }
}
