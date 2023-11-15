using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task CreateProduct(Product product)
        {
             await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product> .Filter.Eq(p=>p.Id, productId);

            DeleteResult deleteResult = await _catalogContext
                                                    .Products
                                                    .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                                & deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext
                     .Products
                     .Find(p=>true)
                     .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, categoryName);

            return await _catalogContext
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

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

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _catalogContext
                                            .Products
                                            .ReplaceOneAsync(g => g.Id == product.Id, product);

            return updateProduct.IsAcknowledged 
                                && updateProduct.ModifiedCount > 0;
        }
    }
}
