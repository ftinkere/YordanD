namespace YordanApi.Requests;

public class CreateLanguageRequest {
    public required string Name { get; set; }
    public string? AutoName { get; set; } = null;
    public string? AutoNameTranscription { get; set; } = null;
    public bool IsPublished { get; set; } = false;
}