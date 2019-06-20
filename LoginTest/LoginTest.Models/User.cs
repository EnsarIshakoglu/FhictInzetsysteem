using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Models
{
    public class User
    {
        [Required(ErrorMessage = "Password field is required!")]
        public string Password { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Username field is required!")]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int TeamId { get; set; }
        public int Id { get; set; }
        public bool IsCompetentForTask { get; set; }
    }
}
