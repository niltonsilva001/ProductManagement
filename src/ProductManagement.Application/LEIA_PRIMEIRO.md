## 📌 Application Layer

Esta camada contém a **lógica de aplicação e transformação de dados**.

### O que DEVE estar aqui:
- ✅ Services (regras de negócio)
- ✅ DTOs (Data Transfer Objects)
- ✅ Validações
- ✅ Mapeamento entre Entity e DTO

### O que NÃO DEVE estar aqui:
- ❌ Acesso direto ao banco (use repositório injetado!)
- ❌ Entity Framework details
- ❌ HTTP/Controllers

### Estrutura de Pastas:
```
ProductManagement.Application/
├── DTOs/
│   ├── CreateProductDto.cs
│   ├── UpdateProductDto.cs
│   └── ProductResponseDto.cs
├── Services/
│   └── ProductService.cs    <- Implementa IProductService
├── Interfaces/
│   └── IProductService.cs   <- Contrato do service
└── ProductManagement.Application.csproj
```

### Dica:
Services recebem Repository injetado e retornam sempre DTOs.
```
ProductService(IProductRepository repository) { ... }
```
