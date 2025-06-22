using YordanApi.Contracts.Repositories;
using YordanApi.Domain.Entity;
using YordanApi.Domain.ValueObjects;
using YordanApi.Requests;

namespace YordanApi.Services;

public class LanguageService(ILanguageRepository languageRepository) {
    public async Task<IEnumerable<Language>> GetAllLanguagesAsync() {
        return (await languageRepository.GetAllAsync()).Select(l => l.ToDomain());
    }

    public async Task<Language?> GetLanguageByIdAsync(Guid id) {
        return (await languageRepository.GetById(id))?.ToDomain();
    }

    public async Task<Language?> CreateLanguageAsync(CreateLanguageRequest request, Guid authorId) {
        var language = new Language(authorId) {
            Name = new Translatable(request.Name, request.AutoName, request.AutoNameTranscription),
            IsPublished = request.IsPublished,
        };
        return await AddLanguageAsync(language) > 0 ? language : null;
    }

    public Task<int> AddLanguageAsync(Language language) {
        // TODO add articles, pos
        return languageRepository.AddAsync(language.ToDbo());
    }

    public Task<int> UpdateLanguageAsync(Language language) {
        // TODO add articles, pos
        return languageRepository.UpdateAsync(language.ToDbo());
    }

    public Task<int> DeleteLanguageAsync(Guid id) {
        return languageRepository.DeleteAsync(id);
    }
}