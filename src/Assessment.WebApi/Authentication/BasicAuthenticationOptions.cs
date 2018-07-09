using Microsoft.AspNetCore.Authentication;

namespace Assessment.WebApi.Authentication
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }

}
