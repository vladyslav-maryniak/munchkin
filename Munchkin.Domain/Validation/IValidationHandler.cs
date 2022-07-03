namespace Munchkin.Domain.Validation
{
    public interface IValidationHandler { }

    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
