using Niqash.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Niqash.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [EmailAddress(ErrorMessageResourceName = "validEmailFormat", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [EmailAddress(ErrorMessageResourceName = "validEmailFormat", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Email { get; set; }

        [Display(Name = "password", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(ModelsResource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "firstName", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string FirstName { get; set; }

        [Display(Name = "lastName", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(25, ErrorMessageResourceName = "max25", ErrorMessageResourceType = typeof(ModelsResource))]
        public string LastName { get; set; }

        [Display(Name = "email", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [EmailAddress(ErrorMessageResourceName = "validEmailFormat", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Email { get; set; }

        [Display(Name = "password", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [StringLength(100, ErrorMessageResourceName ="min6",ErrorMessageResourceType =typeof(ModelsResource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [EmailAddress(ErrorMessageResourceName = "validEmailFormat", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Email { get; set; }

        [Display(Name = "password", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [StringLength(100, ErrorMessageResourceName = "min6", ErrorMessageResourceType = typeof(ModelsResource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmPassword", ResourceType = typeof(ModelsResource))]
        [Compare("Password", ErrorMessageResourceName = "confirmPasswordErrorMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "email", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [EmailAddress(ErrorMessageResourceName = "validEmailFormat", ErrorMessageResourceType = typeof(ModelsResource))]
        public string Email { get; set; }
    }
}
