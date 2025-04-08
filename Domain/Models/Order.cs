using Domain.Enums;
using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required, MaxLength(10)]
    public OrderStatus Status { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Required]
    public int OrderShippingAddressId { get; set; } // Foreign key to ShippingAddress
    public ShippingAddress ShippingAddress { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Payment Payment { get; set; }
}
