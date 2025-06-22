using LinqToDB.Mapping;

namespace YordanApi.Dbos;

[Table(Name = "Images")]
public class ImageDbo {

    [PrimaryKey] public Guid Id { get; set; } = Guid.CreateVersion7();
    [Column]
    public required Guid OwnerId { get; set; }
    [Column]
    public Guid? AuthorId { get; set; }
    [Column]
    public required string Name { get; set; }
    [Column]
    public required string Base64 { get; set; }
}