using AutoMapper;
using BookStoreAPI.Entities.DTOs;
using BookStoreAPI.Entities;

namespace BookStoreAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BookStore, BookStoreDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
