using AutoMapper;
using LojaApi.Data.Dto;
using LojaApi.Models;

namespace LojaApi.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<PedidoInputDto, Pedido>();
            CreateMap<ProdutoOutputDto, Produto>();
        }
    }
}
