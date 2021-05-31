﻿using Domain.Base;

namespace Domain.Entities
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}