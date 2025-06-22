using LinqToDB;
using YordanApi.Dbos;

namespace YordanApi.Repositories;

public class ImageRepository(AppDataConnection db) {
    public Task<int> AddImage(ImageDbo image) {
        return db.InsertAsync(image);
    }

    public Task<int> AddAvatar(Guid userId, string base64) {
        var image = new ImageDbo {
            OwnerId = userId,
            AuthorId = userId,
            Name = "avatar",
            Base64 = base64,
        };
        return db.InsertAsync(image);
    }

    public Task<ImageDbo?> GetAvatar(Guid userId) {
        return db.Images
            .Where(i => i.Name == "avatar" && i.OwnerId == userId)
            .OrderByDescending(i => i.Id)
            .FirstOrDefaultAsync();
    }
}