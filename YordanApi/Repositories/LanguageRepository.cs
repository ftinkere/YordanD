using LinqToDB;
using YordanApi.Contracts.Repositories;
using YordanApi.Dbos;

namespace YordanApi.Repositories;

public class LanguageRepository(AppDataConnection db) : ILanguageRepository {
    public async Task<IEnumerable<LanguageDbo>> GetAllAsync() {
        return await db.Languages.ToListAsync();
    }

    public Task<LanguageDbo?> GetById(Guid id) {
        return db.Languages.FirstOrDefaultAsync(l => l.Id == id);
    }

    public Task<int> AddAsync(LanguageDbo language) {
        return db.InsertAsync(language);
    }

    public Task<int> UpdateAsync(LanguageDbo language) {
        return db.UpdateAsync(language);
    }

    public Task<int> DeleteAsync(Guid id) {
        return db.Languages.DeleteAsync(l => l.Id == id);
    }
}