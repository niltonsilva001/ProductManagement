## 📌 API Layer

Esta camada é a **porta de entrada** da aplicação - Controllers e configuração.

### O que DEVE estar aqui:
- ✅ Controllers
- ✅ Roteamento HTTP
- ✅ Injeção de Dependência (Program.cs)
- ✅ Configuração de Startup
- ✅ Middleware

### O que NÃO DEVE estar aqui:
- ❌ Lógica de negócio
- ❌ Acesso direto ao banco
- ❌ Implementação de repositórios

### Estrutura de Pastas:
```
ProductManagement.API/
├── Controllers/
│   └── ProductsController.cs   <- Endpoints HTTP
├── Program.cs                  <- Startup e DI
├── appsettings.json
└── ProductManagement.API.csproj
```

### Dica:
Controllers recebem IProductService injetado:
```csharp
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetAll()
    {
        // Usar _productService aqui
    }
}
```

**Nunca faça new ProductService() ou acesso ao banco no controller!**
