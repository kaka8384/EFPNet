using System.ComponentModel.DataAnnotations;

namespace EFPNet.ViewModel
{
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住登录用户
        /// </summary>
        [Display(Name = "记住登录")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// 登录成功后返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
