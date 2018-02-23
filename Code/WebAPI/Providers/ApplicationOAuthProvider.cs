using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Data.Infrastructure;
using Data.Repositories;



// This file is originally from the SPA template that ships with
// Visual Studio Express 2013 for Web

namespace WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly Helpers.SecurityHelper _securityHelper;
        private  Services.UserService _userService;


        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }


            _publicClientId = publicClientId;
            _securityHelper = new Helpers.SecurityHelper();
            InitializeUserService();
                }

        private void InitializeUserService()
        {
            DbFactory _dbFactory = new DbFactory();
            IUnitOfWork unitOfWork = new UnitOfWork(_dbFactory);
            IUserRepository userRepository = new Data.Repositories.UserRepository(_dbFactory);
            _userService = new Services.UserService(userRepository, unitOfWork);

        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //TODO: Add user check
            InitializeUserService();
            string json = string.Empty;
            Model.User User = _userService.UserLogin(context.UserName, _securityHelper.Md5Encryption(context.Password));
            if (User != null)
            {
                if (User.IsActive)
                {
                    ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    oAuthIdentity.AddClaim(new Claim("RoleId", User.RoleID.ToString()));
                    oAuthIdentity.AddClaim(new Claim("UserId", User.UserID.ToString()));
                    AuthenticationProperties properties = CreateProperties("WebAPI");
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("InActiveUser");
                    return;
                }

            }
            else
            {
                context.SetError("Invalid_request");
                return;
            }

        }


        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties( string name)
        {

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"Name",name}
            };
            return new AuthenticationProperties(data);
        }
    }
}