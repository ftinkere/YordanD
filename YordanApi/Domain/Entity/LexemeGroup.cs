using Microsoft.CodeAnalysis.CSharp.Syntax;
using YordanD.Domain.ValueObjects;

namespace YordanD.Domain.Entity;

public class LexemeGroup {
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    public IEnumerable<Lexeme> Lexemes { get; set; } = new List<Lexeme>();
    
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public GramList? GramList { get; set; } = null;
}