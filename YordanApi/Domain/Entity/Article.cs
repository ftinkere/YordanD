using YordanD.Domain.ValueObjects;

namespace YordanD.Domain.Entity;

public class Article(Transcriptable lemma) {
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Transcriptable Lemma { get; set; } = lemma;
    public IEnumerable<Tuple<Transcriptable,GramList?>> Forms { get; set; } = new List<Tuple<Transcriptable,GramList?>>();
    public IEnumerable<Image> Images { get; set; } = new List<Image>();
    
    public RichText Description { get; set; } = new RichText("");
    
    public IEnumerable<LexemeGroup> LexemeGroups { get; set; } = new List<LexemeGroup>();

    public void AddForm(Transcriptable form, GramList? gramList) {
        Forms = Forms.Append(Tuple.Create(form, gramList));
    }
    
    public void AddImage(Image image) {
        Images = Images.Append(image);
    }
    
    public void AddLexemeGroup(LexemeGroup lexemeGroup) {
        LexemeGroups = LexemeGroups.Append(lexemeGroup);
    }
}