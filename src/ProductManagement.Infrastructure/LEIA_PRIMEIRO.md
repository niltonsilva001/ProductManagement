## 📌 Infrastructure Layer

Esta camada contém **implementações concretas** de tecnologias externas.

### O que DEVE estar aqui:
- ✅ Entity Framework Core DbContext
- ✅ Implementação de Repositories
- ✅ Migrations
- ✅ Configurações de EF (FluentAPI, etc)
- ✅ Serviços de acesso a dados

### O que NÃO DEVE estar aqui:
- ❌ Lógica de negócio complexa (vai no Service!)
- ❌ Controllers/HTTP
- ❌ DTOs

### Estrutura de Pastas:
```
ProductManagement.Infrastructure/
├── Data/
│   ├── ApplicationDbContext.cs    <- Seu DbContext
│   └── Migrations/                <- Geradas pelo EF
├── Repositories/
│   └── ProductRepository.cs       <- Implementa IProductRepository
└── ProductManagement.Infrastructure.csproj
```

### Dica:
ProductRepository implementa IProductRepository (do Domain).
Use LINQ aqui para queries:
```csharp
public async Task<List<Product>> GetAllAsync()
{
    return await _dbContext.Products
        .Where(p => !p.IsDeleted)
        .OrderBy(p => p.Name)
        .ToListAsync();
}
```
