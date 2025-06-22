using YordanApi.Dbos;
using YordanApi.Domain.ValueObjects;

namespace YordanApi.Domain.Entity;

public class Language(Guid authorId) {
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required Translatable Name { get; set; }
    public RichText Description { get; set; } = new RichText("");
    public bool IsPublished { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid AuthorId { get; set; } = authorId;
    public IEnumerable<Guid> EditorsIds { get; set; } = new List<Guid>();
    public IEnumerable<PartOfSpeech> PartsOfSpeech { get; set; } = new List<PartOfSpeech>();
    public IEnumerable<Article> Articles { get; set; } = new List<Article>();
    
    public static Language Create(Guid authorId, Translatable name, bool isPublished = false) {
        return new Language(authorId) { Name = name, IsPublished = isPublished };
    }
    
    public void AddEditor(Guid editorId) {
        EditorsIds = EditorsIds.Append(editorId);
    }
    
    public void AppendPartOfSpeech(PartOfSpeech partOfSpeech) {
        PartsOfSpeech = PartsOfSpeech.Append(partOfSpeech);
    }
    
    public void RemovePartOfSpeech(Guid partOfSpeechId) {
        PartsOfSpeech = PartsOfSpeech.Where(pos => pos.Id != partOfSpeechId);
    }

    public void ReorderPartOfSpeech(Guid partOfSpeechId, int newIndex) {
        if (newIndex >= PartsOfSpeech.Count())
            return;
        
        var listWithout = PartsOfSpeech.Where(pos => pos.Id != partOfSpeechId);
        var added = listWithout.InsertAfter(newIndex - 1, PartsOfSpeech.Single(pos => pos.Id == partOfSpeechId));
        PartsOfSpeech = added;
    }
    
    public void AddArticle(Article article) {
        Articles = Articles.Append(article);
    }

    public Language ToLight() {
        return new Language(AuthorId) {
            Id = Id,
            Name = Name,
            Description = Description,
            IsPublished = IsPublished,
        };
    }

    public void Update(string? name, string? autoName, string? autoNameTranscription, bool? isPublished, string? description) {
        Name = new Translatable(name ?? Name.Content, autoName ?? Name.Translation, autoNameTranscription ?? Name.Transcription);
        Description = new RichText(description ?? Description.Content);
        IsPublished = isPublished ?? IsPublished;
    }
}

public static class LanguageExtensions {
    public static Language ToDomain(this LanguageDbo dbo) {
        return new Language(dbo.AuthorId) {
            Id = dbo.Id,
            Name = new Translatable(dbo.Name, dbo.AutoName, dbo.AutoNameTranscription),
            Description = new RichText(dbo.Description),
            IsPublished = dbo.IsPublished,
            CreatedAt = dbo.CreatedAt,
            EditorsIds = dbo.EditorsIds.ToList(),
            // TODO pos, articles
        };
    }

    public static LanguageDbo ToDbo(this Language domain) {
        return new LanguageDbo {
            Id = domain.Id,
            AuthorId = domain.AuthorId,
            Name = domain.Name.Content,
            AutoName = domain.Name.Translation,
            AutoNameTranscription = domain.Name.Transcription,
            Description = domain.Description.Content,
            IsPublished = domain.IsPublished,
            CreatedAt = domain.CreatedAt,
            EditorsIds = domain.EditorsIds.ToArray(),
        };
    }
}