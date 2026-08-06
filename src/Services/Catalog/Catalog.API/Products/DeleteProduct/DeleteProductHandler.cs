namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Calling DeleteProductCommandHandler.Handle for delete product with id: {ProductId}", command.id);
        var product = await session.LoadAsync<Product>(command.id);
        if (product is null)
        {
            throw new ProductNoFoundException();
        }
        session.Delete<Product>(product);
        await session.SaveChangesAsync();
        return new DeleteProductResult(true);
    }
}
