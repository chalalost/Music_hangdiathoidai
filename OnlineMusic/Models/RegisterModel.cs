using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineMusic.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
        [Required(ErrorMessage = "Yêu cầu nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        public string Email { get; set; }
        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string Phone { get; set; }
    }
}