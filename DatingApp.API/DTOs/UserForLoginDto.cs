﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForLoginDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password {get; set;}
    }
}