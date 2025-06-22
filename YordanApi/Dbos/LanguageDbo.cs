using LinqToDB.Mapping;

namespace YordanApi.Dbos;

[Table(Name = "Languages")]
public class LanguageDbo {
    [PrimaryKey]
    public required Guid Id { get; set; }
    [Column]
    public required Guid AuthorId { get; set; }
    [Column(DbType = "uuid[]")]
    public Guid[] EditorsIds { get; set; } = [];
    [Column]
    public required string Name { get; set; }
    [Column]
    public string? AutoName { get; set; } = null;
    [Column]
    public string? AutoNameTranscription { get; set; } = null;
    [Column]
    public required string Description { get; set; }
    [Column]
    public required bool IsPublished { get; set; }
    [Column(DbType = "timestamptz")]
    public DateTime CreatedAt { get; set; }
}