﻿using FakeFutbin.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FakeFutbin.Api.Data;

public class FakeFutbinDbContext: DbContext
{
    public FakeFutbinDbContext(DbContextOptions<FakeFutbinDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Players
        //Brazil Nationality
        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 1,
            Name = "Carlos Henrique Casimiro",
            Age = 30,
            Raiting = 89,
            Position = "Central Defensive Midfilder",
            ImageURL = "/Images/Brazil/Brazil1.jpg",
            MarketValue = 30000,
            Qty = 15,
            NationalityId = 1
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 2,
            Name = "Gabriel Jesus",
            Age = 25,
            Raiting = 89,
            Position = "Striker",
            ImageURL = "/Images/Brazil/Brazil2.jpg",
            MarketValue = 35000,
            Qty = 20,
            NationalityId = 1
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 3,
            Name = "Marcelo Vieira da Silva Júnior",
            Age = 34,
            Raiting = 94,
            Position = "Left Back",
            ImageURL = "/Images/Brazil/Brazil3.jpg",
            MarketValue = 500000,
            Qty = 5,
            NationalityId = 1
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 4,
            Name = "Neymar da Silva Santos Jr.",
            Age = 30,
            Raiting = 96,
            Position = "Left Winger",
            ImageURL = "/Images/Brazil/Brazil4.jpg",
            MarketValue = 264000,
            Qty = 7,
            NationalityId = 1
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 5,
            Name = "Vinícius José de Oliveira Júnior",
            Age = 22,
            Raiting = 96,
            Position = "Left Winger",
            ImageURL = "/Images/Brazil/Brazil5.jpg",
            MarketValue = 221000,
            Qty = 13,
            NationalityId = 1
        });

        //England Nationality
        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 6,
            Name = "Steven Gerrard",
            Age = 42,
            Raiting = 92,
            Position = "Central Midfilder",
            ImageURL = "/Images/England/England1.jpg",
            MarketValue = 215000,
            Qty = 4,
            NationalityId = 2
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 7,
            Name = "Frank Lampard",
            Age = 44,
            Raiting = 91,
            Position = "Central Midfilder",
            ImageURL = "/Images/England/England2.jpg",
            MarketValue = 132000,
            Qty = 6,
            NationalityId = 2
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 8,
            Name = "Wayne Rooney",
            Age = 36,
            Raiting = 93,
            Position = "Striker",
            ImageURL = "/Images/England/England3.jpg",
            MarketValue = 349000,
            Qty = 8,
            NationalityId = 2
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 9,
            Name = "Jadon Sancho",
            Age = 22,
            Raiting = 91,
            Position = "Right Midfilder",
            ImageURL = "/Images/England/England4.jpg",
            MarketValue = 110000,
            Qty = 20,
            NationalityId = 2
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 10,
            Name = "Paul Scholes",
            Age = 47,
            Raiting = 92,
            Position = "Central Midfilder",
            ImageURL = "/Images/England/England5.jpg",
            MarketValue = 117000,
            Qty = 6,
            NationalityId = 2
        });

        //France Nationality
        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 11,
            Name = "Karim Benzema",
            Age = 34,
            Raiting = 98,
            Position = "Central Forward",
            ImageURL = "/Images/France/France1.jpg",
            MarketValue = 230000,
            Qty = 9,
            NationalityId = 3
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 12,
            Name = "Eric Cantona",
            Age = 56,
            Raiting = 94,
            Position = "Central Forward",
            ImageURL = "/Images/France/France2.jpg",
            MarketValue = 390000,
            Qty = 5,
            NationalityId = 3
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 13,
            Name = "Thierry Henry",
            Age = 45,
            Raiting = 94,
            Position = "Striker",
            ImageURL = "/Images/France/France3.jpg",
            MarketValue = 562000,
            Qty = 8,
            NationalityId = 3
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 14,
            Name = "Claude Makélélé",
            Age = 49,
            Raiting = 91,
            Position = "Central Defensive Midfilder",
            ImageURL = "/Images/France/France4.jpg",
            MarketValue = 110000,
            Qty = 10,
            NationalityId = 3
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 15,
            Name = "Zinedine Zidane",
            Age = 50,
            Raiting = 97,
            Position = "Central Attacking Midfilder",
            ImageURL = "/Images/France/France5.jpg",
            MarketValue = 1581000,
            Qty = 3,
            NationalityId = 3
        });

        //Italy Nationality
        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 16,
            Name = "Fabio Cannavaro",
            Age = 48,
            Raiting = 93,
            Position = "Central Back",
            ImageURL = "/Images/Italy/Italy1.jpg",
            MarketValue = 195000,
            Qty = 6,
            NationalityId = 4
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 17,
            Name = "Giorgio Chiellini",
            Age = 38,
            Raiting = 96,
            Position = "Central Back",
            ImageURL = "/Images/Italy/Italy2.jpg",
            MarketValue = 137000,
            Qty = 11,
            NationalityId = 4
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 18,
            Name = "Alessandro Del Piero",
            Age = 47,
            Raiting = 93,
            Position = "Central Forward",
            ImageURL = "/Images/Italy/Italy3.jpg",
            MarketValue = 155000,
            Qty = 13,
            NationalityId = 4
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 19,
            Name = "Gennaro Gattuso",
            Age = 44,
            Raiting = 90,
            Position = "Central Defensive Midfilder",
            ImageURL = "/Images/Italy/Italy4.jpg",
            MarketValue = 100000,
            Qty = 15,
            NationalityId = 4
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 20,
            Name = "Andrea Pirlo",
            Age = 43,
            Raiting = 93,
            Position = "Central Midfilder",
            ImageURL = "/Images/Italy/Italy5.jpg",
            MarketValue = 179000,
            Qty = 12,
            NationalityId = 4
        });

        //Spain Nationality
        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 21,
            Name = "Iker Casillas",
            Age = 41,
            Raiting = 93,
            Position = "Goalkeeper",
            ImageURL = "/Images/Spain/Spain1.jpg",
            MarketValue = 339000,
            Qty = 5,
            NationalityId = 5
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 22,
            Name = "Andrés Iniesta Luján",
            Age = 38,
            Raiting = 83,
            Position = "Central Midfilder",
            ImageURL = "/Images/Spain/Spain2.jpg",
            MarketValue = 10000,
            Qty = 13,
            NationalityId = 5
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 23,
            Name = "Sergio Ramos García",
            Age = 36,
            Raiting = 88,
            Position = "Central Back",
            ImageURL = "/Images/Spain/Spain3.jpg",
            MarketValue = 8500,
            Qty = 17,
            NationalityId = 5
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 24,
            Name = "Raúl González Blanco",
            Age = 45,
            Raiting = 93,
            Position = "Central Forward",
            ImageURL = "/Images/Spain/Spain4.jpg",
            MarketValue = 105000,
            Qty = 5,
            NationalityId = 5
        });

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = 25,
            Name = "Fernando Torres",
            Age = 38,
            Raiting = 92,
            Position = "Striker",
            ImageURL = "/Images/Spain/Spain5.jpg",
            MarketValue = 156000,
            Qty = 9,
            NationalityId = 5
        });

        //Add Nationalities
        modelBuilder.Entity<PlayerNationality>().HasData(new PlayerNationality
        {
            Id = 1,
            Name = "Brazil",
            ImageURL = "/Images/Nationalities/Nationality1.jpg"
        });

        modelBuilder.Entity<PlayerNationality>().HasData(new PlayerNationality
        {
            Id = 2,
            Name = "England",
            ImageURL = "/Images/Nationalities/Nationality2.jpg"
        });

        modelBuilder.Entity<PlayerNationality>().HasData(new PlayerNationality
        {
            Id = 3,
            Name = "France",
            ImageURL = "/Images/Nationalities/Nationality3.jpg"
        });

        modelBuilder.Entity<PlayerNationality>().HasData(new PlayerNationality
        {
            Id = 4,
            Name = "Italy",
            ImageURL = "/Images/Nationalities/Nationality4.jpg"
        });

        modelBuilder.Entity<PlayerNationality>().HasData(new PlayerNationality
        {
            Id = 5,
            Name = "Spain",
            ImageURL = "/Images/Nationalities/Nationality5.jpg"
        });
    }

    public DbSet<UserPlayer> UserPlayers { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerNationality> PlayerNationalities { get; set; }
    public DbSet<User> Users { get; set; }
}
