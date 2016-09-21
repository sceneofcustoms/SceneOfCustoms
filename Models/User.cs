using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SceneOfCustoms.Models
{
    public class User
    {
        //[Required]
        //[EmailAddress]
        //[StringLength(150)]
        // [Display(Name = "Email:")]
        //[Remote("doesEmailExist", "User")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        public string NAME { get; set; } 

        //[Required]
        //[DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6)]
        //[Display(Name = "Lösenord:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string PASSWORD { get; set; }
    }
}