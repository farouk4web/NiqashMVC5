using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Niqash.Resources;

namespace Niqash.Models
{
    // model was use in app
    public class EditProfileModel
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

        [Display(Name = "aboutMe", ResourceType = typeof(ModelsResource))]
        [MinLength(2, ErrorMessageResourceName = "min2", ErrorMessageResourceType = typeof(ModelsResource))]
        [MaxLength(200, ErrorMessageResourceName = "max200", ErrorMessageResourceType = typeof(ModelsResource))]
        public string AboutMe { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "oldPassword", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "newPassword", ResourceType = typeof(ModelsResource))]
        [Required(ErrorMessageResourceName = "requiredMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        [StringLength(100, ErrorMessageResourceName = "min6", ErrorMessageResourceType = typeof(ModelsResource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirmNewPassword", ResourceType = typeof(ModelsResource))]
        [Compare("NewPassword", ErrorMessageResourceName = "confirmPasswordErrorMsg", ErrorMessageResourceType = typeof(ModelsResource))]
        public string ConfirmPassword { get; set; }
    }



    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}