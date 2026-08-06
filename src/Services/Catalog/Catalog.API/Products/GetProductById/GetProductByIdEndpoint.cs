
namespace Catalog.API.Products.GetProductById;


public record GetProductResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductResponse>();
            if(response is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Retrieves a product by its ID.")
        .WithDescription("Returns the details of a product identified by its Product ID.");
    }
}
