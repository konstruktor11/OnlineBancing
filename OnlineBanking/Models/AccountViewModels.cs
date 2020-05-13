using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineBanking.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Address of e-mail")]
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

        [Display(Name = "To memorize a browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Address of e-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Address of e-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Improper address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "To memorize me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Write your name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Length of the name must be from 4 to 20 characters")]
        //[Remote("CheckKlName", "Account")]
        public string KlName { get; set; }

        [Required]
        [Display(Name = "Write your last name")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Length of the surname must be from 4 to 20 characters")]
        public string KlSurname { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Field length must be from 4 to 30 characters")]
        [Display(Name = "Write number of your passport")]
        public string KlPassportNum { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Field length must be from 4 to 30 characters")]
        [Display(Name = "Your telephone number")]
        public string KlPhone { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Field length must be from 4 to 150 characters")]
        [Display(Name = "Write address where you live now")]
        public string KlAddress {get; set;}

       
      
        [Display(Name = "Sum on your account")]
        public decimal KlBalance { get; set; }


        [Display(Name = "History transaction  of money")]
        public string Istor { get; set; }



        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Improper address")]
        [Display(Name = "Address of e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value {0} must contain no less {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation of password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "A password and his confirmation does not coincide.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Improper address")]
        [Display(Name = "Address of e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value {0} must contain no less {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation of password")]
       [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "A password and his confirmation does not coincide.")]
       public string ConfirmPassword { get; set; }


        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Improper address")]
        [Display(Name = "Mail")]
        public string Email { get; set; }
    }
}
