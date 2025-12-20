using CSharpFunctionalExtensions;
using DmFors.WebApi.Shared.Results;

namespace DmFors.WebApi.Shared.CQS;

public interface IQuery;

public interface IQueryHandler<TResult, in TQuery> where TQuery : IQuery
{
    Task<Result<TResult, Errors>> Handle(TQuery query, CancellationToken ct);
}