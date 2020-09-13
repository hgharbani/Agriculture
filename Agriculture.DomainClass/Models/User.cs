﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agriculture.DomainClass.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ActiveCode { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public int HumanId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Human Human { get; set; }

    }
}
