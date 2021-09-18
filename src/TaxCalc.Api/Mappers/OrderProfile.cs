using AutoMapper;
using TaxCalc.Api.Dtos;
using TaxCalc.Domain.Models;

namespace TaxCalc.Api.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>();
        }
    }
}
