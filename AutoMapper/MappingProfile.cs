using AutoMapper;
using myproject.Data;
using myproject.Models;

namespace myproject.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
           CreateMap<Product,ProductDetailsDto>().ReverseMap();
        }
    }

}