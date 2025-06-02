using AutoMapper;
using LojaApi.Data.Dto;
using LojaApi.Models;

namespace LojaApi.Profiles
{
    public class CaixaProfile : Profile
    {
        public CaixaProfile()
        {
            CreateMap<CaixaDto, Caixa>();
            CreateMap<DimensoesDto, Dimensoes>();
        }
    }
}
