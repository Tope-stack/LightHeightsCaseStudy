using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual BookStore BookStore { get; set; }
        public int Quantity { get; set; }
        [Range(1, 1000)]
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
