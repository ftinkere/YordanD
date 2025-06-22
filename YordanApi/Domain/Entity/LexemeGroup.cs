using YordanApi.Domain.ValueObjects;

namespace YordanApi.Domain.Entity;

public class LexemeGroup {
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public IEnumerable<Lexeme> Lexemes { get; set; } = new List<Lexeme>();
    
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public GramList? GramList { get; set; } = null;
}