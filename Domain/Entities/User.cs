using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string Bio { get; set; }
    public string ProfilePic { get; set; }
    public virtual ICollection<Post> Posts { get; set; }

}
