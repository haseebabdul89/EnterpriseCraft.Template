using System.Threading.Tasks;

namespace EnterpriseCraft.Template.Shared.Contracts;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query);
}
