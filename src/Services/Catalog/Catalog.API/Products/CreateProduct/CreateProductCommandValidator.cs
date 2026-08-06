namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=> x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Product category is required.");
        RuleFor(x=> x.ImageFile)
            .Empty().WithMessage("Image file is not allowed for this command.");
        RuleFor(x=> x.Price)
            .NotEmpty().WithMessage("Product price is required")
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");
    }
}
