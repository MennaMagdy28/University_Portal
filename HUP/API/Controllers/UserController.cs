using System.Security.Claims;
using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService service)
    {
        _userService = service;
    }

    //GET : api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersListResponse>>> GetAll()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }
    //Get : api/User/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileInfoDto>> GetById(Guid id)
    {
        var profile = await _userService.GetUserById(id);
        if (profile == null)
            return BadRequest("User not found.");
        return Ok(profile);
    }

    [HttpPatch("insert-profile-data")]
    public async Task<IActionResult> InsertMissingData([FromBody] UpdateInfoDto dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        bool result = await _userService.InsertMissingData(userId, dto);
        if (!result) return BadRequest("Failed to update");
        return Ok("Profile updated successfully.");
    }

    [HttpPatch("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateInfoDto dto)
    {
        Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        bool result = await _userService.Update(userId, dto);
        if (!result) return BadRequest("Failed to update");
        return Ok("Profile updated successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var exist = await _userService.Exists(dto.NationalId);
        if (exist) return BadRequest("This national Id is already registered");
        await _userService.AddAsync(dto);
        return Ok("User added successfully.");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var user = await _userService.SoftDelete(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }
    
    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDelete(Guid id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
            return NotFound();
        await _userService.Remove(id);
        return NoContent();
    }
}