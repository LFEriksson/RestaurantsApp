using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infrastructure.Authorization;

public class RestaurantsClaimsPrincipalFactory(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<AppUser, IdentityRole>(userManager, roleManager, options)
{

    public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
    {
        var id = await GenerateClaimsAsync(user);

        var dateOfBirthThreshold = new DateOnly(1900, 1, 1);
        if (user.DateOfBirth > dateOfBirthThreshold)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}