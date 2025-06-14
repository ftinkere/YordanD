namespace YordanD.Domain.ValueObjects;

public record Transcriptable(string Content, string Transcription, string? Adaptation = null);