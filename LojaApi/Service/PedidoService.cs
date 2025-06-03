using LojaApi.Data;
using LojaApi.Data.Dto;
using LojaApi.Interface;
using LojaApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace LojaApi.Service
{
    public class PedidoService : IPedido
    {
        private readonly DataBaseContext _dbContext;

        public PedidoService(DataBaseContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
        }
        public bool CadastrarPedido(List<PedidoInputDto> pedidosDto)
        {
            List<Pedido> pedidos = pedidosDto.Select(dto => new Pedido
            {
                Produtos = dto.Produtos.Select(p => new Produto
                {
                    ProdutoId = p.Produto_id,
                    Dimensoes = new Dimensoes
                    {
                        Altura = p.Dimensoes.Altura,
                        Largura = p.Dimensoes.Largura,
                        Comprimento = p.Dimensoes.Comprimento
                    }
                }).ToList()
            }).ToList();

            _dbContext.Pedidos.AddRange(pedidos);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<ProdutoOutputDto> ConsultarPedidos()
        {
            return _dbContext.Produtos.Select(p => new ProdutoOutputDto
            {
                ProdutoId = p.ProdutoId,
                Dimensoes = new Dimensoes
                {
                    Altura = p.Dimensoes.Altura,
                    Largura = p.Dimensoes.Largura,
                    Comprimento = p.Dimensoes.Comprimento
                },
                PedidoId = p.PedidoId
            }).ToList();
        }

        public List<PedidoEmbalado> ProcessarPedidos(List<PedidoInputDto> pedidosDto)
        {
            List<PedidoEmbalado> pedidoEmbalados = new List<PedidoEmbalado>();

            CaixaService caixaService = new CaixaService(_dbContext);

            var caixas = caixaService.ConsultarCaixas();

            var embalados = new List<PedidoEmbaladoDto>();

            if (CadastrarPedido(pedidosDto))
            {
                foreach (var pedido in pedidosDto)
                {
                    var produtos = pedido.Produtos;

                    int volumeCaixa1 = caixas[0].Dimensoes.Volume;
                    int volumeCaixa2 = caixas[1].Dimensoes.Volume;
                    int volumeCaixa3 = caixas[2].Dimensoes.Volume;

                    List<int> volumesCaixa = new List<int>();

                    volumesCaixa.Add(volumeCaixa1);
                    volumesCaixa.Add(volumeCaixa2);
                    volumesCaixa.Add(volumeCaixa3);

                    var encomenda = new PedidoEmbaladoDto
                    {
                        Caixas = new List<CaixaEmbaladaDto>()
                    };

                    var caixaDisponivel = new CaixaEmbaladaDto
                    {
                        Produtos = new List<string>()
                    };

                    var caixaIndisponivel = new CaixaEmbaladaDto
                    {
                        Caixa_id = "None",
                        Produtos = new List<string>(),
                        Observacao = $"Produto não cabe em nenhuma caixa disponível."
                    };

                    bool embalou = false;

                    foreach (var produto in produtos)
                    {
                        caixaDisponivel = Embalar(produto, volumesCaixa);

                        if (embalou)
                        {
                            encomenda.Caixas.Add(caixaDisponivel);
                        }
                        else
                        {
                            caixaIndisponivel.Produtos.Add(produto.Produto_id);
                            encomenda.Caixas.Add(caixaIndisponivel);
                        }
                    }

                    embalados.Add(encomenda);

                    pedidoEmbalados = embalados.Select(x => new PedidoEmbalado
                    {
                        Caixas = x.Caixas
                        .GroupBy(x => x.Caixa_id)
                        .Select(c => new CaixaEmbalada
                        {
                            Caixa_id = c.Key,
                            Produtos = c.SelectMany(x => x.Produtos).ToList(),
                            Observacao = c.First().Observacao
                        }).ToList()
                    }).ToList();

                    _dbContext.PedidosEmbalados.AddRange(pedidoEmbalados);
                    _dbContext.SaveChanges();

                    #region MetodoEmpacotar
                    CaixaEmbaladaDto Embalar(ProdutoInputDto produto, List<int> volumeCaixas)
                    {
                        int i = 0;
                        var volumeProduto = produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;
                        var caixaUsada = new CaixaEmbaladaDto();

                        foreach (var caixa in caixas)
                        {
                            caixaUsada = new CaixaEmbaladaDto
                            {
                                Produtos = new List<string>()
                            };

                            if (volumeProduto < volumesCaixa[i])
                            {
                                embalou = true;
                                caixaUsada.Caixa_id = caixa.Nome;
                                caixaUsada.Produtos.Add(produto.Produto_id);
                                volumesCaixa[i] -= volumeProduto;
                                
                                i = 0;
                                break;
                            }
                            else
                            {
                                embalou = false;
                                i++;
                            }
                        }

                        return caixaUsada;
                    }
                    #endregion

                }
            }
            else
            {
                throw new Exception("Erro ao empacotar pedidos!");
            }

            return pedidoEmbalados;
        }

    }
}
