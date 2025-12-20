using CSharpFunctionalExtensions;
using DmFors.WebApi.Shared.Results;

namespace DmFors.WebApi.Shared.CQS;

public interface ICommand;


public interface ICommandHandler<TResult, in TCommand> where TCommand : ICommand
{
    Task<Result<TResult, Errors>> Handle(TCommand cmd, CancellationToken ct);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<UnitResult<Errors>> Handle(TCommand cmd, CancellationToken ct);
}