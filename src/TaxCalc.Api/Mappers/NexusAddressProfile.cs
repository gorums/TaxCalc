using AutoMapper;
using TaxCalc.Api.Dtos;
using TaxCalc.Domain.Models;

namespace TaxCalc.Api.Mappers
{
    public class NexusAddressProfile : Profile
    {
        public NexusAddressProfile()
        {
            CreateMap<NexusAddressDto, NexusAddress>();
        }
    }
}
