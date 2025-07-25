using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace demo.ViewModel
{
    public class RegisterVM
    {
        [DisplayName("Tài khoản")]
        public string Username { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không chính xác.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}