﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class UpdateCommentDto
{
    public string? Name { get; set; }
    public string? Content { get; set; }
}
