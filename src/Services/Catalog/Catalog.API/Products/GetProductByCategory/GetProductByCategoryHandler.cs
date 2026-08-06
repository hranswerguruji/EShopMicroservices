namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string category)
    : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>

{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByCategoryQuery for category: {Category}", request.category);
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(request.category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
