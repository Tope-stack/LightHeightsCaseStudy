using AutoMapper;
using BookStoreAPI.DataContext;
using BookStoreAPI.Entities.DTOs;
using BookStoreAPI.Entities;
using BookStoreAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository.Services
{
    public class OrderService : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDTO> OrderStatus(int orderId)
        {
            var order = await _context.Orders.Include(x => x.BookStore).FirstOrDefaultAsync(x => x.OrderId == orderId);
            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<Order> CreateOrder(int bookId, int quantity)
        {
            var book = _context.Books.Where(x => x.BookId == bookId).FirstOrDefault();
            if (book == null)
            {
                return null;
            }
            Order order = new Order
            {
                BookId = bookId,
                Quantity = quantity,
                TotalPrice = book.Price * quantity,
                Status = "Pending"
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<Order>(order);
        }
    }
}
