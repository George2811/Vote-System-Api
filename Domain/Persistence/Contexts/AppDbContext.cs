using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSistem.Domain.Models;
using VotingSistem.Extensions;

namespace VotingSistem.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Vote>().ToTable("Votes");
            builder.Entity<Vote>().HasKey(v => v.VoteId);
            builder.Entity<Vote>().Property(v => v.VoteId).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<Vote>().Property(v => v.Image).IsRequired().HasMaxLength(250);
            //builder.Entity<Vote>().Property(v => v.Choise).IsRequired().HasMaxLength(250);
            builder.Entity<Vote>().Property(v => v.Image).IsRequired().HasColumnType("blob");
            builder.Entity<Vote>().Property(v => v.Choise).IsRequired().HasColumnType("blob");
            builder.Entity<Vote>().Property(v => v.VotingDate).IsRequired();

            builder.Entity<Voter>().ToTable("Voters");
            builder.Entity<Voter>().HasKey(v => v.VoterId);
            builder.Entity<Voter>().Property(v => v.VoterId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Voter>().Property(v => v.Name).IsRequired().HasMaxLength(250);

            // Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }

    }
}
