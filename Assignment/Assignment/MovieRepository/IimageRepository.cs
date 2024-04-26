using Assignment.Models;

namespace Assignment.MovieRepository
{
    public interface IimageRepository
    {
        Task SaveImageAsync(byte[] imageData);
        Task<byte[]> GetImageDataAsync(int id);
    }
}
