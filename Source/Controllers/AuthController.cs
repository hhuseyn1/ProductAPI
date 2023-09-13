using Microsoft.AspNetCore.Mvc;
using Source.Data;
using Source.Models.DTOs;
using Source.Services;

namespace Source.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILoginRegisterService _service;

    public AuthController(AppDbContext context, ILoginRegisterService service)
    {
        this._context = context;
        this._service = service;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserDTO user)
    {
        try
        {
            var token = await _service.Login(user);
            return Ok(token);

        }
        catch (Exception ex)
        {

            return BadRequest(ex);
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserDTO user)
    {
        try
        {
            if (await _service.Register(user))
                return Ok();
            throw new Exception("Something went wrong!");
        }
        catch (Exception ex)
        {

            return BadRequest(ex);
        }
    }
}
