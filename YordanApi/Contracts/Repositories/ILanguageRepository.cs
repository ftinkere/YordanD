using YordanApi.Dbos;

namespace YordanApi.Contracts.Repositories;

public interface ILanguageRepository {
    Task<IEnumerable<LanguageDbo>> GetAllAsync();
    Task<LanguageDbo?> GetById(Guid id);
    Task<int> AddAsync(LanguageDbo language);
    Task<int> UpdateAsync(LanguageDbo language);
    Task<int> DeleteAsync(Guid id);
}