## 📌 Domain Layer

Esta camada contém a lógica **pura de negócio**, sem dependências de frameworks.

### O que DEVE estar aqui:
- ✅ Entidades (Entities)
- ✅ Interfaces/Contratos (IRepository, IService)
- ✅ Value Objects
- ✅ Domínio Driven Design

### O que NÃO DEVE estar aqui:
- ❌ Entity Framework, Database, HTTP
- ❌ DTOs
- ❌ Serviços de aplicação

### Estrutura de Pastas:
```
ProductManagement.Domain/
├── Entities/
│   └── Product.cs          <- Entity do domínio
├── Interfaces/
│   └── IProductRepository.cs <- Contrato (não implementação!)
└── ProductManagement.Domain.csproj
```

### Dica: 
A Entity `Product` NUNCA deve ser usada diretamente na resposta HTTP.
Use DTOs para isso!
