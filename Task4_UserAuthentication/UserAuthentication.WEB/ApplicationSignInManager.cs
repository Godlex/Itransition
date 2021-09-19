namespace UserAuthentication.WEB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Hosting.Internal;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;

    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public ApplicationSignInManager(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<
                SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation,
            ApplicationDbContext context)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _context = context;
        }

        public override async Task SignInWithClaimsAsync(ApplicationUser user, AuthenticationProperties authenticationProperties,
            IEnumerable<Claim> additionalClaims)
        {
            await base.SignInWithClaimsAsync(user, authenticationProperties, additionalClaims);
            user.FirstLogin ??= DateTime.Now;
            user.LastLogin = DateTime.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}