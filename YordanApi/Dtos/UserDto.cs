namespace YordanApi.DTOs;

public class UserDto {
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public string? DisplayName { get; set; }
}
