using System.Collections;
using System.Collections.Generic;
using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SearchController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public SearchController(IKonnichiwaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet("/UserByName/{searchString}")]
    public IActionResult UserByName(string searchString)
    {
        var searchResult = _context.Users.Where(c => c.Name.Contains(searchString)).ToList();
        List<GetUserDto> list = _mapper.Map<List<GetUserDto>>(searchResult);
        return Ok(list);
    }
    [HttpGet("/UserByUserName/{searchString}")]
    public IActionResult UserByUserName(string searchString)
    {
        var searchResult = _context.Users.Where(c => c.UserName.Contains(searchString)).ToList();
        List<GetUserDto> list = _mapper.Map<List<GetUserDto>>(searchResult);
        return Ok(list);
    }
    [HttpGet("/PostByUser/{userId}/{searchString}")]
    public IActionResult PostByUser(string searchString,int userId)
    {
        var searchResult = _context.post.Where
            (c => c.UserId == userId && (c.Body.Contains(searchString)) || c.Name.Contains(searchString))
            .ToList();
        List<GetUserDto> list = _mapper.Map<List<GetUserDto>>(searchResult);
        return Ok(list);
    }
    [HttpGet("/Post/{searchString}")]
    public IActionResult Post(string searchString)
    {
        var searchResult = _context.Users.Where(c => c.Name.Contains(searchString) || c.Name.Contains(searchString)).ToList();
        return Ok(searchResult);
    }
    [HttpGet("/CommentByPost/{postId}/{searchString}")]
    public IActionResult CommentByPost(string searchString, int postId)
    {
        var searchResult = _context.Comment.Where
            (c => c.PostId == postId && c.Content.Contains(searchString))
            .ToList();
        return Ok(searchResult);
    }
    [HttpGet("/Comment/{searchString}")]
    public IActionResult Comment(string searchString)
    {
        var searchResult = _context.Comment.Where(c => c.Content.Contains(searchString)).ToList();
        return Ok(searchResult);
    }
}
