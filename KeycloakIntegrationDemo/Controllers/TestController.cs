using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok(new { message = "Este es un endpoint público." });
    }

    [HttpGet("private")]
    [Authorize] // Requiere autenticación con Keycloak
    public IActionResult PrivateEndpoint()
    {
        return Ok(new { message = "Este es un endpoint protegido con Keycloak." });
    }

    [HttpGet("role-based")]
    [Authorize(Roles = "user")] // Requiere que el usuario tenga el rol "admin"
    public IActionResult RoleBasedEndpoint()
    {
        return Ok(new { message = "Este endpoint es accesible solo para users." });
    }
}
