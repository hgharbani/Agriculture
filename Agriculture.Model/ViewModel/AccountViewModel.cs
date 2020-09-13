using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agriculture.Model.ViewModel
{
   public class RegisterViewModel
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string LastName { get; set; }

        [DisplayName("شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

        [DisplayName("کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string NationalNumber { get; set; }

        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string UserName { get; set; }


        [DisplayName(" ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [DataType(DataType.Password)] 
        [Compare("Password",ErrorMessage = "رمز عبور عبور وارد شده همخوان نیستند")]
        public string RePassword { get; set; }

    }

   public class LoginViewModel
   {

       [DisplayName(" ایمیل")]
       [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
       [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
       public string Email { get; set; }
       [DisplayName("رمز عبور")]
       [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
       [DataType(DataType.Password)]
       public string Password { get; set; }

       [DisplayName("مرا به خاطر بسپار")]
       public bool RememmberMe { get; set; }
   }
}
