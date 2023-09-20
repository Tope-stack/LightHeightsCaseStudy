using BookStoreAPI.Entities.DTOs;
using BookStoreAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        protected ResponseDTO _response;
        private IBookStoreRepository _bookStoreRepo;

        public BookStoreController(IBookStoreRepository bookStoreRepo)
        {
            _bookStoreRepo = bookStoreRepo;
            this._response = new ResponseDTO();
        }

        /// <summary>
        /// Get all available books in store
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<BookStoreDTO> books = await _bookStoreRepo.GetBooksInStore();
                _response.Result = books;
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
        /// Get a book by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                BookStoreDTO book = await _bookStoreRepo.GetBookById(id);
                _response.Result = book;
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
        /// Create and add a book to the store
        /// </summary>
        /// <param name="bookStoreDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<object> CreateBook([FromBody] BookStoreDTO bookStoreDTO)
        {
            try
            {
                BookStoreDTO model = await _bookStoreRepo.AddBookToStore(bookStoreDTO);
                _response.Result = model;
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
        /// Remove a book from the store 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> DeleteBook(int id)
        {
            try
            {
                bool isSuccess = await _bookStoreRepo.RemoveBookFromStore(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
