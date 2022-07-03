using MediatR;
using Munchkin.Domain.Validation;
using System.Net;

namespace Munchkin.Domain.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
        where TResponse : CqrsResponse, new()
    {
        private readonly IValidationHandler<TRequest>? validationHandler;

        // Have 2 constructors incase the validator does not exist
        public ValidationBehaviour()
        {
        }

        public ValidationBehaviour(IValidationHandler<TRequest> validationHandler)
        {
            this.validationHandler = validationHandler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (validationHandler is not null)
            {
                var result = await validationHandler.Validate(request);
                if (result.IsSuccessful == false)
                {
                    return new TResponse { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = result.ErrorMessage };
                }
            }

            return await next();
        }
    }
}
