using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using ProjectDemoRecipes.Models;

namespace ProjectDemoRecipes.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Call the base implementation

            // Favorite Relationships
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Recipe)
                .WithMany()
                .HasForeignKey(f => f.RecipeId);

            // Review Relationships
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Recipe)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RecipeId);

            modelBuilder.Entity<Recipe>().HasData(
       new Recipe
       {
           Id = 1,
           Title = "Spaghetti Bolognese",
           Description = "A classic Italian pasta dish.",
           CookingTime = 30,
           IsVegetarian = false,
           IsLactoseFree = true,
           ImageUrl = "spaghetti.jpg"
       },
       new Recipe
       {
           Id = 2,
           Title = "Vegetarian Lasagna",
           Description = "Delicious and cheesy vegetarian lasagna.",
           CookingTime = 45,
           IsVegetarian = true,
           IsLactoseFree = false,
           ImageUrl = "lasagna.jpg"
       },

       new Recipe
       {
           Id = 3,
           Title = "Ikea Meatballs",
           Description = "Meatballs with mashed potatoes and lingonberry jam",
           CookingTime = 50,
           IsVegetarian = false,
           IsLactoseFree = false,
           ImageUrl = "ikeameatballs.jpg"
       }

    );
        }

    }
}
