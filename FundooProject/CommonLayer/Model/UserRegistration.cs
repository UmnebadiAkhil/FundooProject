using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegistration
    {
        //User Registration Entity

        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression("^[A-Z][a-z]{1,}", ErrorMessage = "Name should start with capital letters and" +
            "should contain atleast 2 characters.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression("^[A-Z][a-z]{1,}", ErrorMessage = "Name should start with capital letters and" +
            "should contain atleast 2 characters.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
