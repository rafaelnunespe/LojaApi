using System.Collections.Generic;
using LojaApi.Data;
using LojaApi.Data.Dto;
using LojaApi.Interface;
using LojaApi.Models;
using Microsoft.EntityFrameworkCore;

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
                PedidoId = dto.Pedido_Id,
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
            _dbContext.SaveChanges();

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

        public List<PedidoEmbalado> ProcessarPedidos()
        {
            List<PedidoEmbalado> pedidoEmbalados = null;

            CaixaService caixaService = new CaixaService(_dbContext);

            List<ProdutoOutputDto> produtos = ConsultarPedidos();
            var caixas = caixaService.ConsultarCaixas();
            List<CaixaEmbalada> caixasEmbaladas;

            var embalados = new List<PedidoEmbalado>();

            foreach (var produto in produtos)
            {
                Caixa caixa1 = caixas[0];
                Caixa caixa2 = caixas[1];
                Caixa caixa3 = caixas[2];

                var novaEmbalagem = new PedidoEmbalado
                {
                    Pedido_id = produto.PedidoId,
                    Caixas = new List<CaixaEmbalada>()
                };

                var caixaIndisponivel = new CaixaEmbalada
                {
                    Caixa_id = "None",
                    Produtos = new List<string>(),
                    Observacao = $"Produto {produto.ProdutoId} não cabe em nenhuma caixa disponível."
                };

                #region Encaixotamento
                if (Encaixotar(produto.Dimensoes, caixa1.Dimensoes))
                {
                    var caixaUsada = new CaixaEmbalada
                    {
                        Caixa_id = caixa1.Nome,
                        Produtos = new List<string>()
                    };

                    caixaUsada.Produtos.Add(produto.ProdutoId);
                    novaEmbalagem.Caixas.Add(caixaUsada);
                }
                else if (Encaixotar(produto.Dimensoes, caixa2.Dimensoes))
                {
                    var caixaUsada = new CaixaEmbalada
                    {
                        Caixa_id = caixa2.Nome,
                        Produtos = new List<string>()
                    };

                    caixaUsada.Produtos.Add(produto.ProdutoId);

                    novaEmbalagem.Caixas.Add(caixaUsada);
                }
                else if (Encaixotar(produto.Dimensoes, caixa3.Dimensoes))
                {
                    var caixaUsada = new CaixaEmbalada
                    {
                        Caixa_id = caixa3.Nome,
                        Produtos = new List<string>()
                    };

                    caixaUsada.Produtos.Add(produto.ProdutoId);

                    novaEmbalagem.Caixas.Add(caixaUsada);
                }
                else
                {
                    caixaIndisponivel.Produtos.Add(produto.ProdutoId);

                    novaEmbalagem.Caixas.Add(caixaIndisponivel);
                }
                #endregion

                embalados.Add(novaEmbalagem);

                pedidoEmbalados = embalados;
            }

            //_dbContext.PedidosEmbalados.AddRange(pedidoEmbalados);
            //_dbContext.SaveChanges();

            return pedidoEmbalados;

        }

        private bool Encaixotar(Dimensoes produto, Dimensoes caixa)
        {
            if (produto.Volume < caixa.Volume)
            {
                if (produto.Altura < caixa.Altura && produto.Largura <
                    caixa.Largura && produto.Comprimento < caixa.Comprimento)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public Dimensoes AtualizaMedidas(Caixa caixa, Produto produto)
        {
            caixa.Dimensoes.Altura = caixa.Dimensoes.Altura - produto.Dimensoes.Altura;
            caixa.Dimensoes.Largura = caixa.Dimensoes.Largura - produto.Dimensoes.Largura;
            caixa.Dimensoes.Comprimento = caixa.Dimensoes.Comprimento - produto.Dimensoes.Comprimento;

            return caixa.Dimensoes;
        }
    }
}
