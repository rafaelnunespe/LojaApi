
# 📦 LojaAPi

## ⚙️ Pré-requisitos

1. Instale o **SQL Server**.
2. Mantenha a configuração do usuário `sa` com a seguinte connection string no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=./;Database=DB_Loja;User Id=sa;Password=Banana123+;TrustServerCertificate=true"
}
```

## Executando o Projeto

Abra a aplicação no **Visual Studio** e inicie o projeto. A interface **Swagger** será carregada automaticamente.

---

## ✅ Passo a Passo

### 1. Cadastrar Modelos de Caixas

Acesse o endpoint `cadastroCaixa` no Swagger e cadastre os seguintes modelos **um por vez**:

#### Caixa 1

```json
{
  "id": 0,
  "nome": "Caixa 1",
  "dimensoes": {
    "altura": 30,
    "largura": 40,
    "comprimento": 80
  }
}
```

#### Caixa 2

```json
{
  "id": 0,
  "nome": "Caixa 2",
  "dimensoes": {
    "altura": 80,
    "largura": 50,
    "comprimento": 40
  }
}
```

#### Caixa 3

```json
{
  "id": 0,
  "nome": "Caixa 3",
  "dimensoes": {
    "altura": 50,
    "largura": 80,
    "comprimento": 60
  }
}
```

---

### 2. Cadastrar Pedidos

Acesse o endpoint `cadastroPedido` e envie o seguinte JSON:

```json
[
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "PS5", "dimensoes": { "altura": 40, "largura": 10, "comprimento": 25 } },
      { "produto_id": "Volante", "dimensoes": { "altura": 40, "largura": 30, "comprimento": 30 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Joystick", "dimensoes": { "altura": 15, "largura": 20, "comprimento": 10 } },
      { "produto_id": "Fifa 24", "dimensoes": { "altura": 10, "largura": 30, "comprimento": 10 } },
      { "produto_id": "Call of Duty", "dimensoes": { "altura": 30, "largura": 15, "comprimento": 10 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Headset", "dimensoes": { "altura": 25, "largura": 15, "comprimento": 20 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Mouse Gamer", "dimensoes": { "altura": 5, "largura": 8, "comprimento": 12 } },
      { "produto_id": "Teclado Mecânico", "dimensoes": { "altura": 4, "largura": 45, "comprimento": 15 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Cadeira Gamer", "dimensoes": { "altura": 120, "largura": 60, "comprimento": 70 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Webcam", "dimensoes": { "altura": 7, "largura": 10, "comprimento": 5 } },
      { "produto_id": "Microfone", "dimensoes": { "altura": 25, "largura": 10, "comprimento": 10 } },
      { "produto_id": "Monitor", "dimensoes": { "altura": 50, "largura": 60, "comprimento": 20 } },
      { "produto_id": "Notebook", "dimensoes": { "altura": 2, "largura": 35, "comprimento": 25 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Jogo de Cabos", "dimensoes": { "altura": 5, "largura": 15, "comprimento": 10 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Controle Xbox", "dimensoes": { "altura": 10, "largura": 15, "comprimento": 10 } },
      { "produto_id": "Carregador", "dimensoes": { "altura": 3, "largura": 8, "comprimento": 8 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "Tablet", "dimensoes": { "altura": 1, "largura": 25, "comprimento": 17 } }
    ]
  },
  {
    "pedido_id": 0,
    "produtos": [
      { "produto_id": "HD Externo", "dimensoes": { "altura": 2, "largura": 8, "comprimento": 12 } },
      { "produto_id": "Pendrive", "dimensoes": { "altura": 1, "largura": 2, "comprimento": 5 } }
    ]
  }
]
```

---

### 3. Empacotar Pedidos

Após cadastrar os pedidos, execute o método `empacotarPedido` no Swagger.

Esse método processará todos os pedidos e tentará alocar os produtos nas caixas cadastradas, otimizando o espaço e respeitando os limites de dimensão.
