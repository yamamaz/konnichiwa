using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AddLikeController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public AddLikeController(IKonnichiwaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost]
    [Authorize]
    public IActionResult LikePost(AddLikeDto addLike)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        Likes like = _context.Like.FirstOrDefault(x => user.Id == user.Id && x.PostId == addLike.PostId);
        if (like != null)
        {
            _context.Like.Remove(like);
            _context.SaveChanges();
            return Ok("liked Removed");
        }
        like = _mapper.Map<Likes>(addLike);
        like.UserId = user.Id;
        _context.Like.Add(like);
        _context.SaveChanges();
        return Ok("liked");
    }

}
