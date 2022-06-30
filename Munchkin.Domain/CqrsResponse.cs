using System.Net;

namespace Munchkin.Domain
{
    public record CqrsResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string? ErrorMessage { get; init; }
    }
}
