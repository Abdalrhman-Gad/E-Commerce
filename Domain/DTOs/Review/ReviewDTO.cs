﻿namespace Domain.DTOs.Review
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
    }
}
