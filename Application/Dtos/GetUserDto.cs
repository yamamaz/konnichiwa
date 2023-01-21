using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos;
public class GetUserDto
{
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ProfilePic { get; set; }
}
