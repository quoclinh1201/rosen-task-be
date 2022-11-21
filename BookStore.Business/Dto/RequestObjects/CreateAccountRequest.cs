using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Business.Dto.RequestObjects
{
    public class CreateAccountRequest
    {

        [Required]
        [MaxLength(20)]
        public string? Username { get; set; } 

        [Required]
        [MaxLength(20)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(20)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string? ConfirmPassword { get; set; }

  
    }
}
