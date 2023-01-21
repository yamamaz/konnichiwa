using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ImageUploadController : Controller
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;

    public ImageUploadController(IKonnichiwaDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost("ImageUpload/{userId}")]
    public ActionResult Post([FromForm] UploadImageDto file,int userId)
    {
        try
        {
            var fileName = DateTime.Now.Millisecond + file.FormFile.FileName;
            var profilePicUrl = @"Files\Uploads\" + fileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"Files\Uploads\", fileName);
            using (Stream stream = new FileStream(path,FileMode.Create))
            {
                file.FormFile.CopyTo(stream);
            }
            var currentUser = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == currentUser);
            user.ProfilePic = profilePicUrl;
            _context.SaveChanges();
            return Ok(path);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
