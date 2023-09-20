using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Entities
{
    public class BookStore
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
