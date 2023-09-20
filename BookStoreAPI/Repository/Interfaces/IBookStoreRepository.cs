using BookStoreAPI.Entities.DTOs;

namespace BookStoreAPI.Repository.Interfaces
{
    public interface IBookStoreRepository
    {
        Task<BookStoreDTO> AddBookToStore(BookStoreDTO bookStoreDTO);
        Task<BookStoreDTO> GetBookById(int id);
        Task<IEnumerable<BookStoreDTO>> GetBooksInStore();
        Task<bool> RemoveBookFromStore(int id);
    }
}
