using YordanApi.Domain.ValueObjects;

namespace YordanApi.Domain.Entity;

public class Lexeme(RichText shortDescription) {
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public EstimateLine Path { get; set; } = new EstimateLine();
    
    public RichText Short { get; set; } = shortDescription;
    public RichText? Long { get; set; } = null;
    
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    public GramList? GramList { get; set; } = null;
}