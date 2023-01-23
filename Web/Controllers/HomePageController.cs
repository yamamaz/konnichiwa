using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Application.Dtos;
using AutoMapper;
using Application.Interface;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : Controller
    {
        private readonly IKonnichiwaDbContext _context;
        private readonly IMapper _mapper;

        public HomePageController(IKonnichiwaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPosts(int page = 1, int pageSize = 10)
        {
            var posts = _context.post
                .Include(p => p.User)
                .OrderByDescending(p => p.Created)
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)
                .ToList();

            var postDtos = _mapper.Map<List<PostDto>>(posts);

            return Ok(postDtos);
        }
    }
}
