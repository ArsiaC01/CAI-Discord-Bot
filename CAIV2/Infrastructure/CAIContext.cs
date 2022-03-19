using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class CAIContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }   //A Database set is a table in our database. Get set means we can fetch all servers and also modify all servers.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;port=3306;Connect Timeout=5;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }

        public class Server
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }
        }
    }
}
