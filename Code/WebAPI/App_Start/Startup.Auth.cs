using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using WebAPI.Providers;
using Owin;
using System.Web;
using System.Web.SessionState;

namespace WebAPI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            String PublicClientId = "self";
            
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                AllowInsecureHttp = true
            };

            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);


        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);          
        }
    }
}