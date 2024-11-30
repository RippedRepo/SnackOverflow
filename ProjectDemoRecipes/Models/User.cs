using Microsoft.AspNetCore.Identity;

namespace ProjectDemoRecipes.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}