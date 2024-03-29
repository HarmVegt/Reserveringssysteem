﻿using System.Data.Entity;
using System.Data.SqlClient;

namespace Reserveringssysteem
{
    public class ReserveringssysteemContext : DbContext
    {
        public DbSet<Boat> Boats { get; set; }
        public DbSet<BoatType> BoatTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<RecreationalTeam> RecreationalTeams { get; set; }
        public DbSet<MatchTeam> MatchTeams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Result> Results { get; set; }

        public ReserveringssysteemContext() : base("Data Source = 127.0.0.1, 1433; User ID = sa; Password=KBSSE1a2019;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=Reserveringssysteem;Initial Catalog = Reserveringssysteem;")
        {

        }
    }
}
