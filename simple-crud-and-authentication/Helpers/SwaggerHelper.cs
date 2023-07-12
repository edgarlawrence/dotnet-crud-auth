using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace simple_crud_and_authentication.Helpers
{
    public static class SwaggerHelper
    {
        public static Stream SetBearerTokenInSwaggerUi(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var token = request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers["Authorization"] = "Bearer " + token;
            }

            return request.Body;
        }
    }
}
