
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
    public IActionResult GetUser(string username)
    {
        User user = _context.Users.FirstOrDefault(x => x.UserName == username);
        GetUserDto userDto = _mapper.Map<GetUserDto>(user);
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
    public IActionResult UpdateUserProfile(UpdateUserDto updateUser)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        user = _mapper.Map(updateUser, user);
        _context.Users.Update(user);
        _context.SaveChanges();
        return Ok();
    }
    [HttpPut("/user/updateUserPassword/")]
    [Authorize]
    public IActionResult UpdateUserPassword(UpdateUserPasswordDto updateUserPassword)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        user = _mapper.Map(updateUserPassword, user);
        _context.Users.Update(user);
        _context.SaveChanges();
        return Ok();
    }
    [HttpPut("/user/updateUserProfilePic/")]
    [Authorize]
    public IActionResult UpdateUserProfilePic(UpdateUserProfilePicDto updateUserProfilePic)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        user = _mapper.Map(updateUserProfilePic, user);
        _context.Users.Update(user);
        _context.SaveChanges();
        return Ok();
    }
    [HttpDelete("/User/")]
    [Authorize]
    public IActionResult DeletePost(int userId)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        _context.Users.Remove(user);
        _context.SaveChanges();
        return Ok();
    }
}
