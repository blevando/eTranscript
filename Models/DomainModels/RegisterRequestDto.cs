﻿using System.ComponentModel.DataAnnotations;

namespace eTranscript.Models.DomainModels
{

    public class RegisterRequestDto
    {
        [Required]
        public string? Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? MatricNumber { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
