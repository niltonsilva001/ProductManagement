# 📦 Product Management API

> Uma API REST profissional para gestão de produtos, desenvolvida com **.NET 10** seguindo princípios de **Clean Architecture** e **Domain-Driven Design**. Inclui validação robusta, tratamento de erros centralizado e sistema de mensageria com RabbitMQ.

[![.NET 10](https://img.shields.io/badge/.NET-10-purple)](https://dotnet.microsoft.com/)
[![PostgreSQL 15](https://img.shields.io/badge/PostgreSQL-15-blue)](https://www.postgresql.org/)
[![RabbitMQ 3](https://img.shields.io/badge/RabbitMQ-3-orange)](https://www.rabbitmq.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

---

## 🎯 Visão Geral

Este projeto é uma **API REST CRUD** completa para gestão de produtos com:

✅ **Arquitetura em 4 camadas** (Domain, Infrastructure, Application, API)  
✅ **Banco de dados** PostgreSQL com Entity Framework Core  
✅ **Validações robustas** com FluentValidation  
✅ **Tratamento de erros** centralizado via Middleware  
✅ **Mensageria assíncrona** com RabbitMQ (Pub/Sub)  
✅ **Containerização** com Docker Compose  
✅ **Boas práticas** de código limpo e responsabilidade única  

---

## 📋 Recursos Implementados

### 🔧 Core Features

| Feature | Status | Descrição |
|---------|--------|-----------|
| **CRUD Completo** | ✅ | Create, Read, Update, Delete de produtos |
| **Validação de Dados** | ✅ | FluentValidation com regras de negócio |
| **Tratamento de Erros** | ✅ | Middleware global com respostas padronizadas |
| **Mensageria RabbitMQ** | ✅ | Pub/Sub para eventos de produtos |
| **Autenticação** | ⏳ | Em planejamento (JWT) |
| **Testes Unitários** | ⏳ | Em desenvolvimento |
| **Paginação** | ⏳ | Planejado para melhorias |

### 🚀 Endpoints Disponíveis

```
POST   /api/products              Criar novo produto
GET    /api/products              Listar todos os produtos
GET    /api/products/{id}         Obter produto por ID
PUT    /api/products/{id}         Atualizar produto
DELETE /api/products/{id}         Deletar produto
```

---

## 🏗️ Arquitetura

### Estrutura em Camadas

```
ProductManagement/
├── src/
│   ├── ProductManagement.Domain/
│   │   ├── Entities/
│   │   │   └── Product.cs                    # Entidade de domínio
│   │   ├── Events/
│   │   │   ├── ProductCreatedEvent.cs        # Evento de domínio
│   │   │   └── ProductOutOfStockEvent.cs     # Evento de domínio
│   │   └── Interfaces/
│   │       ├── IProductRepository.cs         # Contrato do repositório
│   │       └── IMessagePublisher.cs          # Contrato do publicador
│   │
│   ├── ProductManagement.Infrastructure/
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs       # EF Core DbContext
│   │   │   └── Migrations/                   # Database migrations
│   │   └── Repositories/
│   │       └── ProductRepository.cs          # Implementação do repositório
│   │
│   ├── ProductManagement.Application/
│   │   ├── DTOs/
│   │   │   ├── CreateProductDto.cs
│   │   │   ├── UpdateProductDto.cs
│   │   │   └── ProductResponseDto.cs
│   │   ├── Interfaces/
│   │   │   └── IProductService.cs
│   │   ├── Services/
│   │   │   └── ProductService.cs             # Lógica de negócio
│   │   ├── Validators/
│   │   │   ├── CreateProductValidator.cs
│   │   │   └── UpdateProductValidator.cs
│   │   └── Publishers/
│   │       └── RabbitMqPublisher.cs          # Implementação do publicador
│   │
│   └── ProductManagement.API/
│       ├── Controllers/
│       │   └── ProductsController.cs         # Endpoints REST
│       ├── Middleware/
│       │   └── ExceptionHandlingMiddleware.cs # Tratamento global de erros
│       ├── Consumers/
│       │   └── ProductEventConsumer.cs       # Consumer RabbitMQ
│       ├── HostedServices/
│       │   └── RabbitMqConsumerHostedService.cs # Background service
│       ├── Program.cs                        # Configuração
│       └── appsettings.json                  # Variáveis de ambiente
│
├── docker-compose.yml                        # PostgreSQL + RabbitMQ
├── SETUP.md                                  # Guia de instalação
├── .gitignore                                # Arquivos ignorados
└── README.md                                 # Você está aqui
```

### Responsabilidades por Camada

| Camada | Responsabilidade | Tecnologias |
|--------|------------------|-------------|
| **Domain** | Entidades e contratos (interfaces) | C# puro |
| **Infrastructure** | Acesso a dados, migrações, implementação de repositórios | EF Core, PostgreSQL, RabbitMQ Client |
| **Application** | Lógica de negócio, DTOs, validadores | FluentValidation, Serialização JSON |
| **API** | HTTP, Controllers, Middleware, Configuração | ASP.NET Core, Dependency Injection |

---

## 🛠️ Tecnologias

### Backend
- **.NET 10** - Runtime
- **ASP.NET Core** - Framework web
- **Entity Framework Core 10.0** - ORM
- **Npgsql 10.0.2** - Driver PostgreSQL
- **RabbitMQ.Client 7.2.1** - Messaging
- **FluentValidation 11.x** - Validação

### Banco de Dados
- **PostgreSQL 15** - RDBMS
- **Docker** - Containerização

### Padrões & Princípios
- 🏛️ **Clean Architecture** - Separação clara de responsabilidades
- 🎯 **Domain-Driven Design** - Foco no domínio de negócio
- 🔌 **Repository Pattern** - Abstração da camada de dados
- 💉 **Dependency Injection** - Inversão de controle
- ⚙️ **Middleware Pattern** - Tratamento global de erros
- 📨 **Pub/Sub Pattern** - Mensageria assíncrona

---

## 🚀 Quick Start

### 1️⃣ Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/)
- [Git](https://git-scm.com/)

### 2️⃣ Clonar e Configurar

```bash
git clone https://github.com/SEU_USUARIO/ProductManagement.git
cd ProductManagement

# Copiar arquivos de configuração
cp appsettings.example.json appsettings.json
cp docker-compose.example.yml docker-compose.yml
```

### 3️⃣ Iniciar Docker

```bash
docker-compose up -d
```

### 4️⃣ Aplicar Migrações

```bash
dotnet ef database update --project src/ProductManagement.Infrastructure
```

### 5️⃣ Executar a API

```bash
dotnet run --project src/ProductManagement.API
```

A API estará disponível em `http://localhost:5250`

📖 **[Guia de Instalação Detalhado →](SETUP.md)**

---

## 📊 Exemplos de Uso

### ➕ Criar Produto

```bash
curl -X POST http://localhost:5250/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook Dell",
    "description": "Laptop de alto desempenho",
    "price": 4500.00,
    "stock": 10
  }'
```

**Resposta (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "Notebook Dell",
  "description": "Laptop de alto desempenho",
  "price": 4500.00,
  "stock": 10,
  "active": true
}
```

### 📖 Listar Produtos

```bash
curl http://localhost:5250/api/products
```

### 🔍 Buscar por ID

```bash
curl http://localhost:5250/api/products/550e8400-e29b-41d4-a716-446655440000
```

### ✏️ Atualizar Produto

```bash
curl -X PUT http://localhost:5250/api/products/550e8400-e29b-41d4-a716-446655440000 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook Dell Atualizado",
    "price": 4200.00,
    "stock": 8
  }'
```

### ❌ Deletar Produto

```bash
curl -X DELETE http://localhost:5250/api/products/550e8400-e29b-41d4-a716-446655440000
```

---

## 🔄 Sistema de Mensageria

A API publica eventos de negócio no RabbitMQ automaticamente:

### Eventos Implementados

| Evento | Quando Ocorre | Payload |
|--------|---------------|---------|
| `ProductCreatedEvent` | Novo produto criado | ProductId, ProductName, Price, Stock |
| `ProductOutOfStockEvent` | Estoque atinge zero | ProductId, ProductName |

### Monitorar Eventos

**Console da Aplicação:**
```
info: ProductCreatedEvent received: {
  ProductId: 550e8400-e29b-41d4-a716-446655440000,
  ProductName: Notebook Dell,
  Price: 4500.00,
  Stock: 10
}
```

**UI do RabbitMQ:** http://localhost:15672 (guest/guest)

---

## 📝 Estrutura de Respostas

### Sucesso (2xx)

```json
{
  "id": "uuid",
  "name": "Nome do Produto",
  "description": "Descrição",
  "price": 100.00,
  "stock": 10,
  "active": true
}
```

### Erro (4xx/5xx)

```json
{
  "error": "Descrição do erro",
  "statusCode": 400,
  "timestamp": "2026-06-28T16:28:00Z"
}
```

**Códigos HTTP:**
- `200 OK` - Requisição bem-sucedida
- `201 Created` - Recurso criado
- `204 No Content` - Atualização/Exclusão bem-sucedida
- `400 Bad Request` - Validação falhou
- `404 Not Found` - Recurso não encontrado
- `500 Internal Server Error` - Erro no servidor

---

## 🧪 Testes

### Executar Testes

```bash
dotnet test
```

### Teste Manual (via Postman/Insomnia)

Importe o arquivo `postman_collection.json` (quando disponível) para testar todos os endpoints.

---

## 📚 Aprendizados & Conceitos

Este projeto demonstra:

- ✅ **Repository Pattern** - Como abstrair acesso a dados
- ✅ **Dependency Injection** - Configuração no `Program.cs`
- ✅ **Validação com FluentValidation** - Regras de negócio
- ✅ **Middleware** - Tratamento global de exceções
- ✅ **Domain Events** - Comunicação entre agregados
- ✅ **Pub/Sub Messaging** - Comunicação assíncrona com RabbitMQ
- ✅ **Entity Framework Core** - Migrações e contexto
- ✅ **Async/Await** - Programação assíncrona
- ✅ **DTOs vs Entities** - Proteção do domínio

---

## 🔐 Segurança

- ✅ Credenciais protegidas no `.gitignore`
- ✅ Arquivos `.example.*` como template
- ✅ Senhas padrão apenas para desenvolvimento
- ✅ Tratamento seguro de exceções (sem stack traces em produção)
- ✅ Validação de entrada em todas as camadas

📖 **Veja [SETUP.md](SETUP.md) para configuração de produção**

---

## 🚀 Roadmap

### Fase 2 (Em Planejamento)
- [ ] Autenticação com JWT
- [ ] Testes unitários com xUnit
- [ ] Testes de integração
- [ ] Paginação e filtros avançados
- [ ] Logging estruturado (Serilog)
- [ ] API Documentation (Swagger/OpenAPI)

### Fase 3 (Futuro)
- [ ] Dead Letter Queue para RabbitMQ
- [ ] Circuit Breaker pattern
- [ ] Caching distribuído (Redis)
- [ ] Health Checks
- [ ] Observabilidade (traces distribuídos)

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos:

1. Fork o repositório
2. Crie uma branch (`git checkout -b feature/sua-feature`)
3. Commit suas mudanças (`git commit -m "Add: descrição"`)
4. Push (`git push origin feature/sua-feature`)
5. Abra um Pull Request

---

## 📖 Referências & Documentação

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design by Eric Evans](https://www.domainlanguage.com/ddd/)
- [Entity Framework Core Docs](https://learn.microsoft.com/ef/core/)
- [RabbitMQ Tutorials](https://www.rabbitmq.com/getstarted.html)
- [ASP.NET Core Best Practices](https://learn.microsoft.com/aspnet/core/)

---

## 📄 Licença

Este projeto é licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

---

## 💬 Dúvidas?

Abra uma [Issue](../../issues) ou consulte a [documentação de setup](SETUP.md).

---

**Desenvolvido com ❤️ como projeto de aprendizado em Clean Architecture**
