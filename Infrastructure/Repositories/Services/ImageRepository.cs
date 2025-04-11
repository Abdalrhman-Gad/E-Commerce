using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Services
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor contextAccessor;

        public ImageRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
            this.contextAccessor = contextAccessor;
        }

        public async Task<Image> Upload(Image image)
        {
            ValidateImageFile(image);

            var folderPath = GetImagesFolderPath();
            EnsureFolderExists(folderPath);

            await RemoveExistingImageIfAny(image.Id, folderPath);

            var localFilePath = GetLocalFilePath(folderPath, image.FileName, image.FileExtension);

            await SaveFileAsync(image.File, localFilePath);

            var urlFilePath = GenerateImageUrl(image.FileName, image.FileExtension);

            image.FilePath = urlFilePath;
            await SaveImageRecordAsync(image);

            return image;
        }

        public async Task DeleteAsync(int imageId)
        {
            var image = await db.Images.FindAsync(imageId);

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");
            }

            var folderPath = GetImagesFolderPath();
            var filePath = Path.Combine(folderPath, $"{image.FileName}{image.FileExtension}");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            db.Images.Remove(image);
            await db.SaveChangesAsync();
        }

        #region Private Helper Methods

        private static void ValidateImageFile(Image image)
        {
            if (image.File == null || image.File.Length == 0)
            {
                throw new ArgumentException("Uploaded file is empty or null.");
            }
        }

        private string GetImagesFolderPath()
        {
            return Path.Combine(webHostEnvironment.ContentRootPath, "Images");
        }

        private static void EnsureFolderExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private async Task RemoveExistingImageIfAny(int imageId, string folderPath)
        {
            var existingImage = await db.Images.FindAsync(imageId);

            if (existingImage != null)
            {
                var existingFileName = Path.GetFileName(existingImage.FilePath);
                var existingFilePath = Path.Combine(folderPath, existingFileName);

                if (File.Exists(existingFilePath))
                {
                    File.Delete(existingFilePath);
                }

                db.Images.Remove(existingImage);
                await db.SaveChangesAsync();
            }
        }

        private static string GetLocalFilePath(string folderPath, string fileName, string fileExtension)
        {
            return Path.Combine(folderPath, $"{fileName}{fileExtension}");
        }

        private static async Task SaveFileAsync(IFormFile file, string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        private string GenerateImageUrl(string fileName, string fileExtension)
        {
            var request = contextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{fileName}{fileExtension}";
        }

        private async Task SaveImageRecordAsync(Image image)
        {
            await db.Images.AddAsync(image);
            await db.SaveChangesAsync();
        }

        #endregion
    }
}
