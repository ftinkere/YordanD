namespace YordanApi.Requests;

public class UpdateLanguageRequest {
    public string? Name { get; set; } = null;
    public string? AutoName { get; set; } = null;
    public string? AutoNameTranscription { get; set; } = null;
    public bool? IsPublished { get; set; } = null;
    public string? Description { get; set; } = null;
}