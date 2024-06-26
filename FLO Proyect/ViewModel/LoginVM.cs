﻿using System.ComponentModel.DataAnnotations;

namespace FLO_Proyect.ViewModel
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public bool IsRemember { get; set; }
    }
}
