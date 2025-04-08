using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication;

namespace E_Commerce
{
    public class BearerSecuritySchemeFilter : IDocumentFilter
    {
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public BearerSecuritySchemeFilter(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public async void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            var authenticationSchemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
            if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Description = "Enter JWT Bearer token to access this API"
                };

                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = document.Components.SecuritySchemes ?? new Dictionary<string, OpenApiSecurityScheme>();
                document.Components.SecuritySchemes["Bearer"] = securityScheme;

                foreach (var pathItem in document.Paths.Values)
                {
                    foreach (var operation in pathItem.Operations)
                    {
                        operation.Value.Security.Add(new OpenApiSecurityRequirement
                        {
                            [securityScheme] = Array.Empty<string>()
                        });
                    }
                }
            }
        }
    }
}
