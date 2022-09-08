using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Security;

public static class SecurityServiceRegistration
{
    public static void AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, TokenHelper>();
    }
}