﻿using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class BadgeResource
{
    public int IdBadge { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
}
