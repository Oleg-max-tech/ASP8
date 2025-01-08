using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.DTOs.Inputs;
using OnlineShopping.DTOs.Outputs;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers;

[ApiController]
[Route("api/shop")]
public class ShopController : ControllerBase
{
    private readonly DataContext _context;

    public ShopController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("sayHello")]
    public ActionResult<string> SayHello()
    {
        return Ok("Hello world");
    }

    [HttpGet("users")] // api/shop/users?skip=0&take=10
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var users = await _context.Users
            .Where(u => !u.DeletedAt.HasValue)
            .Select(u => new UserDTO() // Projection
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Email = u.Email,
                Phone = u.Phone
            })
            .Skip(skip) // Paging
            .Take(take)
            .ToArrayAsync(); // async

        // select u.Id, u.FirstName, ...  from Users u;
        
        return Ok(users); // status 200
    }

    [HttpPost("users")]
    public async Task<ActionResult<UserDTO>> AddUser([FromBody] CreateUserDTO createUserDto)
    {
        // add validation
        
        var user = new User()
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            DateOfBirth = createUserDto.DateOfBirth,
            Phone = createUserDto.Phone,
            Email = createUserDto.Email,
            
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userDTO = new UserDTO()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Phone = user.Phone
        };

        return Created($"api/shop/users/{user.Id}", userDTO); // status 201
    }

    [HttpDelete("users/{id}")]
    public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
    {
        var user = await _context.Users.FindAsync(id);
        user.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("users/search")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> SearchUsers([FromQuery] string? name = null, 
                                                                      [FromQuery] string? email = null)
    {
        var users = _context.Users
            .Select(u => new UserDTO() // Projection
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            DateOfBirth = u.DateOfBirth,
            Email = u.Email,
            Phone = u.Phone
        });

        if (name != null)
        {
            users = users.Where(u => (u.LastName + " " + u.FirstName).Contains(name));
        }

        if (email != null)
        {
            users = users.Where(u => u.Email.Contains(email));
        }

        var usersList = await users.ToArrayAsync();
        return Ok(usersList);
    }
    
    // Endpoints
    // 1) get products
    // 2) get users
    // 3) get user's orders
    // 4) create order
    // 5) add, delete, update
}