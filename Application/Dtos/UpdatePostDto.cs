using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class UpdatePostDto
{
    public string? Name { get; set; }
    public string Body { get; set; }
}
