using Microsoft.EntityFrameworkCore;
using YordanApi.Models;

namespace YordanApi;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<ApplicationUser> Users { get; set; }
}