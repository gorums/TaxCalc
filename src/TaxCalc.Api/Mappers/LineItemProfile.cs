using AutoMapper;
using TaxCalc.Api.Dtos;
using TaxCalc.Domain.Models;

namespace TaxCalc.Api.Mappers
{
    public class LineItemProfile : Profile
    {
        public LineItemProfile()
        {
            CreateMap<LineItemDto, LineItem>();
        }
    }
}
