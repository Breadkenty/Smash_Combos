﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using SmashHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmashHub.Core.Cqrs.Users.PostUser
{
    public class PostUserRequest : IRequest<PostUserResponse>
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Usernames can only contain alphanumeric characters (A-Z, 0-9)")]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
