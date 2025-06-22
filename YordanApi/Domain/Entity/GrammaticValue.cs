using YordanD.Domain.ValueObjects;

namespace YordanD.Domain.Entity;

public class GrammaticValue(GrammaticCategory grammaticCategory, Transcriptable name) {
    public Transcriptable Name { get; set; } = name;
    public RichText Description { get; set; } = new RichText("");
}