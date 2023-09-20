using BookStoreAPI.Entities;
using BookStoreAPI.Entities.DTOs;

namespace BookStoreAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(int bookId, int quantity);

        Task<OrderDTO> OrderStatus(int orderId);
    }
}
