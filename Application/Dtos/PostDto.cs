﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string UserName { get; set; }
    public string ProfilePic { get; set; }
    public int LikesCount { get; set; }
}