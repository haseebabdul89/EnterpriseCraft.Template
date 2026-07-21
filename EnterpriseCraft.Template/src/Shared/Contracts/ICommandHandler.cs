using System.Threading.Tasks;

namespace EnterpriseCraft.Template.Shared.Contracts;

public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
{
    Task<TResult> Handle(TCommand command);
}
