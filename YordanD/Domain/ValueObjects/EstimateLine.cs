namespace YordanD.Domain.ValueObjects;

public record EstimateLine {
    private Guid Id { get; } = Guid.CreateVersion7();
    public IEnumerable<int> Path = new List<int> { 1 };
};