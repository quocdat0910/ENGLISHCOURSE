using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class LoginM
    {
        [Key] 
        public int IDCus { get; set; }
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        [StringLength(50, MinimumLength = 6)]
        [DisplayName("Tài khoản")]
        public string NameCus { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string PassCus { get; set; }
    }
}