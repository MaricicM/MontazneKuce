using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MontazneKuce.Models
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
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage ="Morate uneti korisnicko ime")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage ="Morate uneti lozinku")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool ZapamtiMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required(ErrorMessage ="Morate uneti ime")]
        [StringLength(30,ErrorMessage ="Ime mora imati izmedju {0} i {1} slova", MinimumLength =2)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime")]
        [StringLength(30, ErrorMessage = "Prezime mora imati izmedju {2} i {1} slova", MinimumLength = 2)]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Neispravno unet Email")]
        [EmailAddress]
        public string Email { get; set; }

        //[Index("KorisnickoImeIndex", IsUnique =true)]
        [Required(ErrorMessage = "Morate uneti korisnicko ime")]
        [StringLength(30, ErrorMessage = "Korisnicko ime mora imati izmedju {2} i {1} slova", MinimumLength = 5)]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        public bool DrugacijeKorisnickoIme { get; set; }
        

        [Required]
        [StringLength(100, ErrorMessage = "Lozinka mora imati izmedju {2} i {1} karaktera", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Lozinka", ErrorMessage = "Potvrda se ne poklapa sa lozinkom")]
        public string PotvrdaLozinke { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
