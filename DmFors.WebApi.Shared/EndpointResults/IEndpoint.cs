using Microsoft.AspNetCore.Routing;

namespace DmFors.WebApi.Shared.EndpointResults;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}