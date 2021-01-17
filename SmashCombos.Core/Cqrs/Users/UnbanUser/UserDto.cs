﻿using SmashCombos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmashCombos.Core.Cqrs.Users.UnbanUser
{
    public class UserDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public UserType UserType { get; private set; }
    }
}