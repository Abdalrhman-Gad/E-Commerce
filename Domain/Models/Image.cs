using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Image
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }
        
        public string FilePath { get; set; }
    }
}
