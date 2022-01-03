using ApplicationCore.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserRegisterRequestModel
    {
        // Built in validation
        [Required(ErrorMessage = "Email cannot be empty")]      //Automatically add both back-end validation and front-end validation.
        [EmailAddress(ErrorMessage = "Email should be in right format")]
        [StringLength(128)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The password should be at least 8 characters and not exceeding 100", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage =
           "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name cannot be empty")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name cannot be empty")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of Birth cannot be empty")]
        [DataType(DataType.Date)]
        // 01/01/1790
        // 01/01/2020
        // Minimum age and Maximum Age
        [MaximumYear(1900)]     //custom attribute
        public DateTime DateOfBirth { get; set; }
    }
}
