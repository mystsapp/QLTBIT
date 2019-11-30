using System.ComponentModel.DataAnnotations;

namespace QLTB.Models
{
    public class AddPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The new passord and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}