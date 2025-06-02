
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

Abra a aplicação no **Visual Studio 2022**, execute o comando "update-database" no terminal Nuget para criação de banco de dados no SQL e inicie o projeto. A interface **Swagger** será carregada automaticamente.

---

## ✅ Passo a Passo

### 1. Cadastrar Modelos de Caixas

Acesse o endpoint `cadastroCaixas` no Swagger e envie o seguinte JSON:

#### Modelo Caixas

```json
[
  {
    "nome": "Caixa 1",
    "dimensoes": {
      "altura": 30,
      "largura": 40,
      "comprimento": 80
    }
  },
  {
    "nome": "Caixa 2",
    "dimensoes": {
      "altura": 80,
      "largura": 50,
      "comprimento": 40
    }
  },
  {
    "nome": "Caixa 3",
    "dimensoes": {
      "altura": 50,
      "largura": 80,
      "comprimento": 60
    }
  }
]

```

---

### 2. Empacotar Pedidos

Acesse o endpoint `empacotarPedidos` e envie o seguinte JSON:

```json
[
  {
      "pedido_id": 1,
      "produtos": [
        {"produto_id": "PS5", "dimensoes": {"altura": 40, "largura": 10, "comprimento": 25}},
        {"produto_id": "Volante", "dimensoes": {"altura": 40, "largura": 30, "comprimento": 30}}
      ]
    },
    {
      "pedido_id": 2,
      "produtos": [
        {"produto_id": "Joystick", "dimensoes": {"altura": 15, "largura": 20, "comprimento": 10}},
        {"produto_id": "Fifa 24", "dimensoes": {"altura": 10, "largura": 30, "comprimento": 10}},
        {"produto_id": "Call of Duty", "dimensoes": {"altura": 30, "largura": 15, "comprimento": 10}}
      ]
    },
    {
      "pedido_id": 3,
      "produtos": [
        {"produto_id": "Headset", "dimensoes": {"altura": 25, "largura": 15, "comprimento": 20}}
      ]
    },
    {
      "pedido_id": 4,
      "produtos": [
        {"produto_id": "Mouse Gamer", "dimensoes": {"altura": 5, "largura": 8, "comprimento": 12}},
        {"produto_id": "Teclado Mecânico", "dimensoes": {"altura": 4, "largura": 45, "comprimento": 15}}
      ]
    },
    {
      "pedido_id": 5,
      "produtos": [
        {"produto_id": "Cadeira Gamer", "dimensoes": {"altura": 120, "largura": 60, "comprimento": 70}}
      ]
    },
    {
      "pedido_id": 6,
      "produtos": [
        {"produto_id": "Webcam", "dimensoes": {"altura": 7, "largura": 10, "comprimento": 5}},
        {"produto_id": "Microfone", "dimensoes": {"altura": 25, "largura": 10, "comprimento": 10}},
        {"produto_id": "Monitor", "dimensoes": {"altura": 50, "largura": 60, "comprimento": 20}},
        {"produto_id": "Notebook", "dimensoes": {"altura": 2, "largura": 35, "comprimento": 25}}
      ]
    },
    {
      "pedido_id": 7,
      "produtos": [
        {"produto_id": "Jogo de Cabos", "dimensoes": {"altura": 5, "largura": 15, "comprimento": 10}}
      ]
    },
    {
      "pedido_id": 8,
      "produtos": [
        {"produto_id": "Controle Xbox", "dimensoes": {"altura": 10, "largura": 15, "comprimento": 10}},
        {"produto_id": "Carregador", "dimensoes": {"altura": 3, "largura": 8, "comprimento": 8}}
      ]
    },
    {
      "pedido_id": 9,
      "produtos": [
        {"produto_id": "Tablet", "dimensoes": {"altura": 1, "largura": 25, "comprimento": 17}}
      ]
    },
    {
      "pedido_id": 10,
      "produtos": [
        {"produto_id": "HD Externo", "dimensoes": {"altura": 2, "largura": 8, "comprimento": 12}},
        {"produto_id": "Pendrive", "dimensoes": {"altura": 1, "largura": 2, "comprimento": 5}}
      ]
    }
]
```

---

### 3. Empacotar Pedidos

Após cadastrar os pedidos, o retorno aparecerá no retorno em JSON.

Esse método todas as informações dos pedidos no banco de dados SQL.
