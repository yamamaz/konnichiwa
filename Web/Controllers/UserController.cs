
using System.ComponentModel.DataAnnotations;
using Application.Dtos;
using Application.Dtos.UpdateUser;
using Application.Interface;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<User> _validator;
    public UserController(IKonnichiwaDbContext context,IMapper mapper, IValidator<User> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }
    [HttpGet]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

        if (user == null)
        {
            return NotFound();
        }

        var userDto = _mapper.Map<GetUserDto>(user);
        return Ok(userDto);
    }
    [HttpPost]
    public IActionResult PostUser(CreateUserDto createUser)
    {
        var validator = _validator;
        var user = _mapper.Map<User>(createUser);
        var validationResult = _validator.Validate(user);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok();
    }
    [HttpPut("/user/updateProfile")]
    [Authorize]
    [Authorize]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserDto updateUser)
    {
        var currentUser = User.Identity.Name;
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == currentUser);
        if (user == null)
        {
            return NotFound("User not found");
        }

        user = _mapper.Map(updateUser, user);
        _context.Users.Update(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("Error updating user profile: " + ex.Message);
        }

        return Ok();
    }
    [HttpPut("/user/updateUserPassword/")]
    [Authorize]
    public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordDto updateUserPassword)
    {
        try
        {
            var currentUser = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
            user = _mapper.Map(updateUserPassword, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPut("/user/updateUserProfilePic/")]
    [Authorize]
    public async Task<IActionResult> UpdateUserProfilePic(UpdateUserProfilePicDto updateUserProfilePic)
    {
        try
        {
            var currentUser = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
            user = _mapper.Map(updateUserProfilePic, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpDelete("/User/")]
    [Authorize]
    public async Task<IActionResult> DeletePost(int userId)
    {
        try
        {
            var currentUser = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
