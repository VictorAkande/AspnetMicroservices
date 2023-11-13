using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext
                     .Products
                     .Find(p=>true)
                     .ToListAsync();
        }

        public Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await _catalogContext
                            .Products
                            .Find(filter)
                            .ToListAsync();
;        }

        public async Task<Product> GetProductsById(string Id)
        {
            return await _catalogContext
                            .Products
                            .Find(p => p.Id == Id)
                            .FirstOrDefaultAsync();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
