using Catalog.Persistence.Database;
using Catalog.Service.Queries.Contracts;
using Catalog.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Catalog.Service.Queries
{
    public class ProductInStockQueryService : IProductInStockQueryService
    {
        private readonly ApplicationDbContext _context;

        public ProductInStockQueryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DataCollection<ProductInStockDto>> GetAllAsync(int page, int take, IEnumerable<int>? stocks = null)
        {
            var collection = await _context.Stocks
                .Where(x => stocks == null || stocks.Contains(x.ProductInStockId))
                .OrderByDescending(x => x.ProductInStockId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ProductInStockDto>>();
        }

        public async Task<ProductInStockDto> GetAsync(int id)
        {
            return (await _context.Stocks.SingleAsync(x => x.ProductInStockId == id))
                .MapTo<ProductInStockDto>();
        }
    }
}
