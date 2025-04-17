
namespace Domain.DTOs.Review
{
    public class AddReviewDTO
    {
        public int Rating { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }
    }
}
