using LinqToDB;
using Microsoft.EntityFrameworkCore;
using YordanApi.Contracts.Repositories;
using YordanApi.Models;

namespace YordanApi.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository {
    public Task<ApplicationUser?> GetByIdAsync(Guid id) {
        return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(db.Users, u => u.Id == id);
    }

    public Task<ApplicationUser?> GetByUserNameAsync(string userName) {
        return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(db.Users, u => u.UserName == userName);
    }

    public Task<Guid> CreateAsync(ApplicationUser user) {
        db.Users.Add(user);
        return db.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? user.Id : throw new Exception("Failed to create user"));
    }

    public Task<int> UpdateAsync(ApplicationUser user) {
        db.Users.Update(user);
        return db.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(Guid id) {
        return db.Users.Where(u => u.Id == id).DeleteAsync();
    }
}