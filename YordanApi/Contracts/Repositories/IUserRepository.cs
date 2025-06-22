using YordanApi.Models;

namespace YordanApi.Contracts.Repositories;

public interface IUserRepository {
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<ApplicationUser?> GetByUserNameAsync(string userName);
    Task<Guid> CreateAsync(ApplicationUser user);
    Task<int> UpdateAsync(ApplicationUser user);
    Task<int> DeleteAsync(Guid id);
}