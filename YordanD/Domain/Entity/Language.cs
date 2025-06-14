using YordanD.Domain.ValueObjects;

namespace YordanD.Domain.Entity;

public class Language(Guid authorId) {
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public required Transcriptable Name { get; set; }
    public RichText Description { get; set; } = new RichText("");
    public bool IsPublished { get; set; } = false;

    public Guid AuthorId { get; set; } = authorId;
    public IEnumerable<Guid> EditorsIds { get; set; } = new List<Guid>();
    public IEnumerable<PartOfSpeech> PartsOfSpeech { get; set; } = new List<PartOfSpeech>();
    public IEnumerable<Article> Articles { get; set; } = new List<Article>();
    
    public static Language Create(Guid authorId, Transcriptable name, bool isPublished = false) {
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
}