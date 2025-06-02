using AutoMapper;
using LojaApi.Data.Dto;
using LojaApi.Models;

namespace LojaApi.Profiles
{
    public class PedidoEmbaladoProfile : Profile
    {
        public PedidoEmbaladoProfile()
        {
            CreateMap<PedidoEmbaladoDto, PedidoEmbalado>();
        }
    }
}
