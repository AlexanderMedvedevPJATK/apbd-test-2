using System.Data.Common;
using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>().HasData(new List<Item>
        {
            new Item()
            {
                Id = 1,
                Name = "Sword",
                Weight = 100
            },
            new Item()
            {
                Id = 2,
                Name = "Shield",
                Weight = 200
            }
        });
        
        modelBuilder.Entity<Title>().HasData(new List<Title>
        {
            new Title()
            {
                Id = 1,
                Name = "Knight"
            },
            new Title()
            {
                Id = 2,
                Name = "Creep"
            }
        });
        
        modelBuilder.Entity<Character>().HasData(new List<Character>
        {
            new Character()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                CurrentWeight = 0,
                MaxWeight = 1000
            },
            new Character()
            {
                Id = 2,
                FirstName = "Stephen",
                LastName = "King",
                CurrentWeight = 500,
                MaxWeight = 2000
            }
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
        {
            new Backpack()
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 10
            },
            new Backpack()
            {
                CharacterId = 2,
                ItemId = 1,
                Amount = 1
            },
            new Backpack()
            {
                CharacterId = 2,
                ItemId = 2,
                Amount = 2
            }
        });
        
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
        {
            new CharacterTitle()
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Now
            },
            new CharacterTitle()
            {
                CharacterId = 2,
                TitleId = 2,
                AcquiredAt = DateTime.Now
            }
        });
    }

}