using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValdationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<IRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TRequest>
{
    public async Task<TResponse> Handle
        (TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResult =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResult.Where(r=> r.Errors.Any()).SelectMany(r=> r.Errors).ToList();
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return await next();
    }
}
