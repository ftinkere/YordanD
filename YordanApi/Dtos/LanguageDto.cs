using YordanApi.Dbos;
using YordanApi.Domain.Entity;
using YordanApi.Domain.ValueObjects;

namespace YordanApi.DTOs;

public class LanguageDto {
    public required Guid Id { get; set; }
    public required Guid AuthorId { get; set; }
    public IEnumerable<Guid> EditorsIds { get; set; } = new List<Guid>();
    public required string Name { get; set; }
    public string? AutoName { get; set; } = null;
    public string? AutoNameTranscription { get; set; } = null;
    public required string Description { get; set; }
    public required bool IsPublished { get; set; }
    public required DateTime CreatedAt { get; set; }
    
    // public IEnumerable<PartOfSpeech>? PartsOfSpeech { get; set; } = new List<PartOfSpeech>();
    // public IEnumerable<Article>? Articles { get; set; } = new List<Article>();
}

public static class LanguageDtoExtensions {
    public static LanguageDto ToDto(this Language language) {
        return new LanguageDto {
            Id = language.Id,
            AuthorId = language.AuthorId,
            EditorsIds = language.EditorsIds,
            Name = language.Name.Content,
            AutoName = language.Name.Translation,
            AutoNameTranscription = language.Name.Transcription,
            Description = language.Description.Content,
            IsPublished = language.IsPublished,
            CreatedAt = language.CreatedAt,
            // TODO pos, articles
        };
    }
}