using System.Collections.Immutable;
using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfileController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public ProfileController(IKonnichiwaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet("/profile/posts/{userId}")]
    public IActionResult GetPosts(int userId)
    {
        var  posts = _context.post.Where(s => s.UserId == userId).ToList();
        return Ok(posts);
    }
    [HttpGet("/profile/posts/{userId}/Latest")]
    public IActionResult GetPostsOrderByDate(int userId)
    {
        var posts = _context.post.Where(s => s.UserId == userId).ToList();
        posts.Sort((x,y) => y.Created.CompareTo(x.Created));
        return Ok(posts);
    }
    [HttpGet("/profile/posts/{userId}/{fromDate}/to/{toDate}")]
    public IActionResult GetPostsInDates(int userId,DateTime fromDate, DateTime toDate)
    {
        var posts = _context.post.
            Where(s => s.UserId == userId && s.Created < toDate && s.Created > fromDate)
            .ToList();
        posts.Reverse();
        return Ok(posts);
    }
    [HttpGet("/profile/posts/{userId}/MostLiked")]
    public IActionResult MostLiked(int userId)
    {
        var posts = _context.post.Where(s => s.UserId == userId).ToList();
        var likes = _context.Like;
        foreach (var post in posts)
        {
            var numberOf = likes.Where(s => s.PostId == post.Id).Count();
            post.LikesCount = numberOf;
        }
        posts.Sort((x, y) => x.LikesCount.CompareTo(y.LikesCount));
        posts.Reverse();
        return Ok(posts);
    }
    [HttpGet("/profile/posts/{userId}/MostCommented")]
    public IActionResult MostCommented(int userId)
    {
        var posts = _context.post.Where(s => s.UserId == userId).ToList();
        var comments = _context.Comment;
        foreach (var post in posts)
        {
            var numberOf = comments.Where(s => s.PostId == post.Id).Count();
            post.CommentsCount = numberOf;
        }
        posts.Sort((x, y) => x.CommentsCount.CompareTo(y.CommentsCount));
        posts.Reverse();
        return Ok(posts);
    }

    [HttpGet("/profile/users/{userId}")]
    public IActionResult GetUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(s => s.Id == userId);
        GetUserDto userDto = _mapper.Map<GetUserDto>(user);
        return Ok(userDto);
    }
}
