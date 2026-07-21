using FluentValidation;
using MediatR;
using System.Threading.Tasks;

namespace EnterpriseCraft.Template.Shared.Behaviors;

//public class ValidationBehavior
//{
//    // Placeholder for pipeline validation behavior
//    public Task HandleAsync() => Task.CompletedTask;
//}
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new FluentValidation.ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new FluentValidation.ValidationException(failures);
            }
        }

        return await next();
    }
}
