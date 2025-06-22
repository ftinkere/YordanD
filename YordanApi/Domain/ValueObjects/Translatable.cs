namespace YordanApi.Domain.ValueObjects;

public record Transcriptable {
    public string Content { get; init; }
    public string Transcription { get; init; }
    public string? Adaptation { get; init; }
    
    public Transcriptable(string? content = null, string? transcription = null, string? adaptation = null) {
        Content = content;
        Transcription = transcription;
        Adaptation = adaptation;
    }
};