using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UpdateUser
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Bio { get; set; }
    }
}
