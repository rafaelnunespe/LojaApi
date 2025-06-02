using LojaApi.Data;
using LojaApi.Data.Dto;
using LojaApi.Interface;
using LojaApi.Models;

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

        bool embalou = false;

        public List<PedidoEmbalado> ProcessarPedidos(List<PedidoInputDto> pedidosDto)
        {
            List<PedidoEmbalado> pedidoEmbalados = null;

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

                    var caixaIndisponivel = new CaixaEmbaladaDto
                    {
                        Caixa_id = "None",
                        Produtos = new List<string>(),
                        Observacao = $"Produto não cabe em nenhuma caixa disponível."
                    };

                    var caixaUsada = new CaixaEmbaladaDto
                    {
                        Produtos = new List<string>()
                    };

                    foreach (var produto in produtos)
                    {
                        var volumeProduto = produto.Dimensoes.Altura * produto.Dimensoes.Largura * produto.Dimensoes.Comprimento;

                        for (int i = 0; i < caixas.Count; i++)
                        {
                            if (embalou)
                            {
                                volumesCaixa[i] = volumesCaixa[i] - volumeProduto;
                            }

                            if (volumeProduto < volumesCaixa[i])
                            {
                                embalou = true;
                                caixaUsada.Caixa_id = caixas[i].Nome;
                                caixaUsada.Produtos.Add(produto.Produto_id);
                                break;
                            }
                            else
                                embalou = false;
                            
                        }
                        if (!embalou)
                        {
                            caixaIndisponivel.Produtos.Add(produto.Produto_id);
                            encomenda.Caixas.Add(caixaIndisponivel);
                        }
                    }
                    
                    encomenda.Caixas.Add(caixaUsada);

                    embalados.Add(encomenda);
                }
                pedidoEmbalados = embalados.Select(x => new PedidoEmbalado
                {
                    Caixas = x.Caixas.Select(p => new CaixaEmbalada
                    {
                        Caixa_id = p.Caixa_id,
                        Produtos = p.Produtos,
                        Observacao = p.Observacao
                    }).ToList()
                }).ToList();

                _dbContext.PedidosEmbalados.AddRange(pedidoEmbalados);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Erro ao empacotar pedidos!");
            }

            return pedidoEmbalados;

        }
        
    }
}
