namespace YordanApi.Domain.ValueObjects;

public record Tag(string Name) {
    public Color Color {
        get {
            var hash = Name.GetHashCode();
            return (Color)(hash % (int)Color.Zinc); // Adjust the modulo value as needed
        }
    }
};