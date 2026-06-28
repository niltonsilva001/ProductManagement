# 🚀 Guia de Configuração - Product Management API

Bem-vindo! Este guia explica como configurar o projeto localmente para desenvolvimento.

## 📋 Pré-requisitos

- **[.NET 10 SDK](https://dotnet.microsoft.com/download)** ou superior
- **[Docker Desktop](https://www.docker.com/products/docker-desktop)** (para PostgreSQL e RabbitMQ)
- **[Git](https://git-scm.com/)**

## 1️⃣ Clone o Repositório

```bash
git clone https://github.com/SEU_USUARIO/ProductManagement.git
cd ProductManagement
```

## 2️⃣ Configure o Docker (PostgreSQL + RabbitMQ)

### Passo 1: Copie o arquivo de exemplo
```bash
cp docker-compose.example.yml docker-compose.yml
```

### Passo 2: (Opcional) Customize as senhas

Edite `docker-compose.yml` se quiser alterar senhas padrão:

```yaml
environment:
  POSTGRES_PASSWORD: sua_senha_segura  # Padrão: seu valor aqui
  RABBITMQ_DEFAULT_PASS: sua_senha     # Padrão: guest
```

### Passo 3: Inicie os containers

```bash
docker-compose up -d
```

**Verifique se está tudo rodando:**
```bash
docker ps
# Você deve ver 2 containers: postgres e rabbitmq
```

**Acessar a UI do RabbitMQ** (opcional): 
- URL: http://localhost:15672
- Usuário: `guest`
- Senha: `guest`

---

## 3️⃣ Configure a Aplicação .NET

### Passo 1: Copie o arquivo de configuração

```bash
cp appsettings.example.json appsettings.json
```

### Passo 2: Atualize a string de conexão

Edite `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=5432;Database=product_management_db;User Id=postgres;Password=COLOQUE_A_MESMA_SENHA_DO_DOCKER;"
}
```

⚠️ **Importante:** Use a mesma senha que configurou no `docker-compose.yml` (linha `POSTGRES_PASSWORD`).

---

## 4️⃣ Restaure Dependências e Aplique Migrações

### Passo 1: Restaure NuGet packages
```bash
dotnet restore
```

### Passo 2: Aplique as migrations do Entity Framework

```bash
dotnet ef database update --project src/ProductManagement.Infrastructure
```

Você deve ver:
```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (123ms) [Parameters=[], CommandType='Text']
      CREATE TABLE...
```

---

## 5️⃣ Execute a Aplicação

```bash
dotnet run --project src/ProductManagement.API
```

Você verá:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5250
info: Microsoft.Hosting.Lifetime[0]
      Application started.
```

---

## 🧪 Teste a API

### Criar um Produto

```bash
curl -X POST http://localhost:5250/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook",
    "description": "Laptop de alta performance",
    "price": 3500.00,
    "stock": 10
  }'
```

Resposta esperada (201 Created):
```json
{
  "id": "8a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d",
  "name": "Notebook",
  "description": "Laptop de alta performance",
  "price": 3500.00,
  "stock": 10,
  "active": true
}
```

### Listar Todos os Produtos

```bash
curl http://localhost:5250/api/products
```

### Obter um Produto por ID

```bash
curl http://localhost:5250/api/products/8a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d
```

### Atualizar um Produto

```bash
curl -X PUT http://localhost:5250/api/products/8a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook Pro",
    "description": "Versão melhorada",
    "price": 4000.00,
    "stock": 5
  }'
```

### Deletar um Produto

```bash
curl -X DELETE http://localhost:5250/api/products/8a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d
```

---

## 📨 Testar Mensageria RabbitMQ

Quando você cria um produto, uma mensagem é automaticamente publicada no RabbitMQ.

**Verifique no console da aplicação:**
```
info: ProductCreatedEvent received: {ProductId: 8a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4c5d, ProductName: Notebook, Price: 3500}
```

**Ou na UI do RabbitMQ** (http://localhost:15672):
1. Clique em **Queues**
2. Procure pela fila `products.queue`
3. Você verá mensagens lá

---

## 🛑 Parar a Aplicação

- **API**: Pressione `Ctrl + C` no terminal
- **Docker**: Execute `docker-compose down`

---

## 🔧 Solução de Problemas

### ❌ "Cannot connect to the Docker daemon"
**Solução:** Abra o Docker Desktop e aguarde inicializar.

### ❌ "The database file is locked"
**Solução:** Execute `docker-compose down` e depois `docker-compose up -d` novamente.

### ❌ "ConnectionRefusedError: Connection refused on 0.0.0.0:5432"
**Solução:** Verifique se o PostgreSQL está rodando com `docker ps`.

### ❌ "Type arguments cannot be inferred"
**Solução:** Este erro foi resolvido no código. Se persistir, limpe o projeto:
```bash
dotnet clean
dotnet build
```

---

## 📚 Estrutura do Projeto

```
ProductManagement/
├── src/
│   ├── ProductManagement.Domain/          # Entidades e interfaces
│   ├── ProductManagement.Infrastructure/  # BD, repositórios, migrações
│   ├── ProductManagement.Application/     # DTOs, serviços, validadores
│   └── ProductManagement.API/             # Controllers, middleware, consumers
├── appsettings.example.json               # Exemplo de configuração
├── docker-compose.example.yml             # Exemplo de containers
├── .gitignore                             # Arquivos ignorados pelo git
└── SETUP.md                               # Este arquivo
```

---

## 🤝 Contribuindo

1. Crie uma branch: `git checkout -b feature/sua-feature`
2. Commit suas mudanças: `git commit -m "Add: descrição"`
3. Push: `git push origin feature/sua-feature`
4. Abra um Pull Request

---

## 📝 Notas Importantes

- ⚠️ **Nunca faça commit** de `appsettings.json`, `.env` ou `docker-compose.yml` com dados reais
- ✅ Use sempre os arquivos `.example.json` como base
- 🔐 As credenciais padrão (guest/guest) são APENAS para desenvolvimento local
- 🚀 Para produção, configure variáveis de ambiente separadamente

---

## ❓ Dúvidas?

Abra uma **Issue** ou consulte a documentação oficial:
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [RabbitMQ .NET Client](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html)
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core/)

**Boa sorte! 🎉**
