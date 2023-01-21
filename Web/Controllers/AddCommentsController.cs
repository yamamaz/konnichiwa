using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AddCommentsController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public AddCommentsController(IKonnichiwaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost]
    [Authorize]
    public IActionResult Commenting(AddCommentDto addComment)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        Comments comment = _context.Comment.FirstOrDefault(x => x.UserId == user.Id && x.PostId == addComment.PostId);
        comment = _mapper.Map<Comments>(addComment);
        comment.UserId = user.Id;
        comment.DateCreated = DateTime.Now;
        _context.Comment.Add(comment);
        _context.SaveChanges();
        return Ok(comment);
    }
    [HttpGet("{postId}/comments")]
    public IActionResult GetComments(int postId)
    {
        var commnets = _context.Comment.Where(s => s.PostId == postId).ToList();
        commnets.Reverse();
        return Ok(commnets);
    }
    [HttpGet("{postId}/comments/{userId}")]
    public IActionResult GetComment(int postId, int userId)
    {
        var comments = _context.Comment.Where(x => x.UserId == userId && x.PostId == postId).ToList();
        return Ok(comments);
    }
    [HttpDelete("/AddComments/deleteComment/{commentId}")]
    [Authorize]
    public IActionResult DeleteComment(int commentId)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        var comment = _context.Comment.FirstOrDefault(x => x.Id == commentId && x.UserId == user.Id);
        if (comment == null)
        {
            return Unauthorized("this comment is not yours to delete!");
        }
        _context.Comment.Remove(comment);
        _context.SaveChanges();
        return Ok("comment deleted!");
    }
    [HttpPut("/AddComments/{commentId}")]
    [Authorize]
    public IActionResult UpdateComment(UpdateCommentDto updateComment,int commentId)
    {
        var currentUser = User.Identity.Name;
        var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
        var comment = _context.Comment.FirstOrDefault(c => c.Id == commentId && c.UserId == user.Id);
        if (comment == null)
        {
            return Unauthorized("this comment is not yours to edit!");
        }
        comment.UserId = user.Id;
        comment = _mapper.Map(updateComment, comment);
        comment.DateUpdated = DateTime.Now;
        _context.Comment.Update(comment);
        _context.SaveChanges();
        return Ok(comment);
    }
}
