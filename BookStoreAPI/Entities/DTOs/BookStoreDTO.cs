namespace BookStoreAPI.Entities.DTOs
{
    public class BookStoreDTO
    {
        public string BookName { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
