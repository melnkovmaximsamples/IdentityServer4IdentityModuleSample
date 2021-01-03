using System;
using System.Collections.Generic;

namespace IdentityModule.Entities
{
    public class ClientCredential
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string MainScope { get; set; }
        public string RedirectUri { get; set; }
        public string LogoutRedirectUri { get; set; }
        public string GrantType { get; set; }
        public string IdentityModuleUrl { get; set; }
    }
}
