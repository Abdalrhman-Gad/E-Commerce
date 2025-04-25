using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Domain.DTOs.Image
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
