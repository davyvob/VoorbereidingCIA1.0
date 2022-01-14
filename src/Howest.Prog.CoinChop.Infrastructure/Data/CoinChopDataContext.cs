using Howest.Prog.CoinChop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Howest.Prog.CoinChop.Infrastructure.Data
{
    public class CoinChopDataContext : DbContext
    {
        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        public CoinChopDataContext(DbContextOptions<CoinChopDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExpenseGroup>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ExpenseGroup>()
                .Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(19,4)");

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Contributor)
                .WithMany(c => c.Contributions)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .HasOne(e => e.Group)
                .WithMany(c => c.Members)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Member>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);


            modelBuilder.Entity<ExpenseGroup>().HasData(
                new[]
                {
                    new ExpenseGroup
                    {
                        Id = 1, Name = "Friends funpark", Token = "friendsfunpark"
                    }, new ExpenseGroup
                    {
                        Id = 2, Name = "Raiding supplies", Token = "privateers"
                    }
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new[]
                {
                    new Member
                    {
                        Id = 1, Name = "Rachel", Email = "rachel@friends.example.com", GroupId = 1
                    },
                    new Member
                    {
                        Id = 2, Name = "Monica", Email = "monica@friends.example.com", GroupId = 1
                    },
                    new Member
                    {
                        Id = 3, Name = "Chandler", Email = "chandler@friends.example.com", GroupId = 1
                    },
                    new Member
                    {
                        Id = 4, Name = "Joey", Email = "joey@friends.example.com", GroupId = 1
                    },
                    new Member
                    {
                        Id = 5, Name = "Ross", Email = "ross@friends.example.com", GroupId = 1
                    },
                    new Member
                    {
                        Id = 6, Name = "Jan", Email = "jan@kapers.example.com", GroupId = 2
                    },
                    new Member
                    {
                        Id = 7, Name = "Pier", Email = "pieredebeeste@kapers.example.com", GroupId = 2
                    },
                    new Member
                    {
                        Id = 8, Name = "Tjoris", Email = "tjoris@kapers.example.com", GroupId = 2
                    },
                    new Member
                    {
                        Id = 9, Name = "Corneel", Email = "corneel@kapers.example.com", GroupId = 2
                    },
                }
            );

            modelBuilder.Entity<Expense>().HasData(
                new[]
                {
                    new Expense
                    {
                        Id = 1, Amount = 20.0M, ContributorId = 2, Description = "Tickets mom and dad"
                    },
                    new Expense
                    {
                        Id = 2, Amount = 30.0M, ContributorId = 5, Description = "Tickets for myself, bro and sis"
                    },
                    new Expense 
                    {
                        Id = 3, Amount = 20.0M, ContributorId = 1, Description = "Lunch"
                    },
                    new Expense
                    {
                        Id = 4, Amount = 10.0M, ContributorId = 5, Description = "Ice cream"
                    },
                    new Expense
                    {
                        Id = 5, Amount = 10.0M, ContributorId = 3, Description = "Waffles and coffee"
                    },
                    new Expense
                    {
                        Id = 6, Amount = 5.0M, ContributorId = 4, Description = "Souvenir photo"
                    },

                    new Expense
                    {
                        Id = 7, Amount = 200.0M, ContributorId = 7, Description = "Cannonballs"
                    },
                    new Expense
                    {
                        Id = 8, Amount = 40.0M, ContributorId = 8, Description = "Mizzen-mast rigging ropes"
                    },
                    new Expense
                    {
                        Id = 9, Amount = 75.0M, ContributorId = 9, Description = "Heavy loot chest"
                    },
                    new Expense
                    {
                        Id = 10, Amount = 60.0M, ContributorId = 7, Description = "Gunpowder"
                    },
                }
            );
        }
    }
}
