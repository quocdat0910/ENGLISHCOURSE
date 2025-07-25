using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class RegisterM
    {
        [Key]
        public int IDCus { get; set; }
        [Required(ErrorMessage ="Email không được để trống")]
        [DisplayName("Email")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Email không đúng định dạng")]
        public string EmailCus {  get; set; }
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [DisplayName("Tài khoản")]
        [StringLength(50, MinimumLength = 6)]
        public string NameCus { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string PassCus { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [DisplayName("Xác nhận mật khẩu")]
        [Compare("PassCus", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        public string ConfirmPassCus { get; set; }

    }
}