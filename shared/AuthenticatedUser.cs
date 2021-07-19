using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class AuthenticatedUser
{
  private readonly IHttpContextAccessor _accessor;

  public AuthenticatedUser(IHttpContextAccessor accessor)
  {
    _accessor = accessor;
  }
  public string Id => _accessor.HttpContext.User.Identity.Name;

  // public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

  public IEnumerable<Claim> GetClaimsIdentity()
  {
    return _accessor.HttpContext.User.Claims;
  }
}