using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos;
public class UploadImageDto
{
    public IFormFile FormFile { get; set; }
}
