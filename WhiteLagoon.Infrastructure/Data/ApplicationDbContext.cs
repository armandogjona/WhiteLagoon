using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; } //add reference to Villa and create table in Db
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "The royal villa features towering marble columns, gilded ceilings, and expansive windows that open to manicured gardens, seamlessly blending historical grandeur with luxurious comfort.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "The premium villa centers around a stunning infinity pool, with sleek modern architecture, open-air lounge spaces, and floor-to-ceiling glass walls that blend indoor luxury with breathtaking outdoor views.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "The luxury pool villa offers an oasis of elegance, with a private pool surrounded by lush greenery, spacious sun decks, and sophisticated interiors that blend seamless indoor-outdoor living with refined comfort.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                }
            );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number=101,
                    VillaId=1,

                }, 
                new VillaNumber
                {
                    Villa_Number=102,
                    VillaId=1,

                }, 
                new VillaNumber
                {
                    Villa_Number=103,
                    VillaId=1,

                },
                new VillaNumber
                {
                    Villa_Number=104,
                    VillaId=1,

                },
                 new VillaNumber
                 {
                     Villa_Number = 201,
                     VillaId = 2,

                 },
                new VillaNumber
                {
                    Villa_Number = 202,
                    VillaId = 2,

                },
                new VillaNumber
                {
                    Villa_Number = 203,
                    VillaId = 2,

                },
                 new VillaNumber
                 {
                     Villa_Number = 301,
                     VillaId = 3,

                 },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 3,

                }
                );
        }
    }
}
