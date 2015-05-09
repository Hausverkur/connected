﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public virtual Group GroupReference { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}