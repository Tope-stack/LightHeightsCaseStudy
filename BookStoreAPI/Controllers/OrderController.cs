using BookStoreAPI.BackgroundServices;
using BookStoreAPI.Entities.DTOs;
using BookStoreAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMessageProducer _messageProducer;
        protected ResponseDTO _response;

        public OrderController(IOrderRepository orderRespository, IMessageProducer messageProducer)
        {
            _orderRepo = orderRespository;
            _messageProducer = messageProducer;
            this._response = new ResponseDTO();
        }

        /// <summary>
        /// Get the order status
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int orderId)
        {
            try
            {
                OrderDTO order = await _orderRepo.OrderStatus(orderId);
                _response.Result = order;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        /// <summary>
        /// Create/Place a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Invalid Order, Please try again");

                    //var newOrder = await _orderRepo.CreateOrder(order.BookId, order.Quantity);
                }
                var newOrder = await _orderRepo.CreateOrder(order.BookId, order.Quantity);

                if (newOrder == null)
                {
                    return NotFound("Book not available");
                }

                _messageProducer.SendMessage(newOrder);
                _response.Result = newOrder;
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
                return Ok(_response);
            }
            //return StatusCode(500, "Something went wrong, Please try again");
        }
    }
}
