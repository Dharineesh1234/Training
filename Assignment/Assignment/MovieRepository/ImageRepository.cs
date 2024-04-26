using Assignment.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace Assignment.MovieRepository
{
    public class ImageRepository:IimageRepository
    {
        private readonly MoviesContext _db;

        public ImageRepository(MoviesContext db)
        {
            _db = db;
        }
        public async Task SaveImageAsync(byte[] imageData)
        {
            var fileUpload = new FileUpload
            {
                Data = imageData
            };

            _db.FileUploads.Add(fileUpload);
            await _db.SaveChangesAsync();
        }

        public async Task<byte[]> GetImageDataAsync(int id)
        {
            var image = await _db.FileUploads.FindAsync(id);
            if (image == null)
            {
                return null;
            }

            return image.Data;
        }
    }
}
