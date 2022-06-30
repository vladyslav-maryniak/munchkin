using System.Net;

namespace Munchkin.API.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessful(this HttpStatusCode statusCode)
        {
            return ((int)statusCode) >= 200 && ((int)statusCode) < 300;
        }

        public static bool IsFailure(this HttpStatusCode statusCode)
        {
            return !IsSuccessful(statusCode);
        }
    }
}
