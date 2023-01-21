using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Hosting;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PostController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public PostController(IKonnichiwaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost]
    [Authorize]
    public IActionResult CreatePost(CreatePostDto createPost)
    {
        var post = _mapper.Map<Post>(createPost);
        var currentUser = User.Identity.Name;
        var userId = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        post.Created = DateTime.Now;
        post.UserId = userId.Id;
        _context.post.Add(post);
        _context.SaveChanges();
        return Ok(post);
    }
    [HttpGet]
    public IActionResult GetPost(int id)
    {
        var post = _context.post.FirstOrDefault(x => x.Id == id);
        var likes = _context.Like;
        var numberOf = likes.Where(s => s.PostId == post.Id).Count();
        post.LikesCount = numberOf;
        return Ok(post);
    }
    [HttpPut("/post/{postId}")]
    [Authorize]
    public IActionResult UpdatePost(UpdatePostDto updatePost,int postId)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        var post = _context.post.FirstOrDefault(c => c.Id == postId && c.UserId == user.Id);
        if(post == null)
        {
            return Ok("this post is not yours to delete!");
        }
        post = _mapper.Map(updatePost,post);
        post.Updated = DateTime.Now;
        _context.post.Update(post);
        _context.SaveChanges();
        return Ok(post);
    }
    [HttpDelete("/post/deletePost/{postId}")]
    public IActionResult DeletePost(int postId)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        var post = _context.post.FirstOrDefault(c => c.Id == postId && c.UserId == user.Id);
        if (post == null)
        {
            return Ok("this post is not yours to delete!");
        }
        _context.post.Remove(post);
        _context.SaveChanges();
        return Ok();
    }


}
