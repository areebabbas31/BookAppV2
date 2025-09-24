using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data

{
    public class IdentityDbContext
    {
        private DbContextOptions<ApplicationDbContext> options;

        public IdentityDbContext(DbContextOptions<ApplicationDbContext> options) => this.options = options;
    }
}