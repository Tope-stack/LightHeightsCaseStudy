using AutoMapper;
using BookStoreAPI.DataContext;
using BookStoreAPI.Entities.DTOs;
using BookStoreAPI.Entities;
using BookStoreAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository.Services
{
    public class BookStoreService : IBookStoreRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public BookStoreService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<BookStoreDTO> AddBookToStore(BookStoreDTO bookStoreDTO)
        {
            BookStore book = _mapper.Map<BookStoreDTO, BookStore>(bookStoreDTO);
            if (book.BookId > 0)
            {
                _context.Books.Update(book);
            }
            else
            {
                _context.Books.Add(book);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<BookStore, BookStoreDTO>(book);
        }

        public async Task<BookStoreDTO> GetBookById(int id)
        {
            BookStore book = await _context.Books.Where(x => x.BookId == id).FirstOrDefaultAsync();
            return _mapper.Map<BookStoreDTO>(book);
        }

        public async Task<IEnumerable<BookStoreDTO>> GetBooksInStore()
        {
            IEnumerable<BookStore> books = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookStoreDTO>>(books);
        }

        public async Task<bool> RemoveBookFromStore(int id)
        {
            try
            {
                BookStore book = await _context.Books.FirstOrDefaultAsync(u => u.BookId == id);

                if (book == null)
                {
                    return false;
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

