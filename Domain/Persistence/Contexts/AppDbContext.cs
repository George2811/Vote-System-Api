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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Vote>().ToTable("Votes");
            builder.Entity<Vote>().HasKey(v => v.VoteId);
            builder.Entity<Vote>().Property(v => v.VoteId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Vote>().Property(v => v.Image).IsRequired().HasMaxLength(250);
            builder.Entity<Vote>().Property(v => v.Choise).IsRequired().HasMaxLength(250);
            builder.Entity<Vote>().Property(v => v.VotingDate).IsRequired();

            // Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }

    }
}
