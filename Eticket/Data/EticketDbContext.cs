using Eticket.Models;
using ETicket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicket.data
{
    public class EticketDbContext:IdentityDbContext<ApplicationUser>
    {
        public EticketDbContext( DbContextOptions<EticketDbContext> options):base(options)
        { 
        
        }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.MovieId,
                am.ActorId,
            });
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(m => m.Movie)
                .WithMany(am => am.Actor_Movies).HasForeignKey(am=>am.MovieId);

            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a => a.Actor)
                .WithMany(am => am.Actor_Movies).HasForeignKey(am=>am.ActorId);
            base.OnModelCreating(modelBuilder);
            }
        public DbSet<Actor>  Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie>  Actor_Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems  { get; set; }
    }

    
}
