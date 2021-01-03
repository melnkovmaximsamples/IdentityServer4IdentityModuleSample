using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModule.Entities;
using IdentityServer4.Models;

namespace IdentityModule.Shared
{
    public static partial class AppData
    {
        public static class Clients
        {
            public static Lazy<ClientCredential> MinecraftWeb => new Lazy<ClientCredential>(() => new ClientCredential()
            {
                ClientId = "minecraft-web-client-id",
                ClientSecret = "minecraft-web-client-secret",
                RedirectUri = "https://localhost:5001/signin-oidc",
                LogoutRedirectUri = "https://localhost:5001/signout-callback-oidc",
                MainScope = "minecraft-web",
                GrantType = OidcConstants.GrantTypes.AuthorizationCode,
                IdentityModuleUrl = IdentityModule.Url
            });
        }
    }
}
