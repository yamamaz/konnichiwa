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
                .Include(p => p.User) // include the user navigation property
                .OrderByDescending(p => p.Created) // order the posts by descending date
                .Skip((page - 1) * pageSize) //skip the pages before the current one
                .Take(pageSize) // take the current page size
                .ToList(); // convert the result to a list

            var postDtos = _mapper.Map<List<PostDto>>(posts); // map the posts to dtos

            return Ok(postDtos);
        }
    }
}
