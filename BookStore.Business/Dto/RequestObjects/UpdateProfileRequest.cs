using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto.RequestObjects
{
    public class UpdateProfileRequest
    {
        [MaxLength(50)]
        public string? FullName { get; set; }
        
        [MaxLength(50)]
        public string? Email { get; set; }

        [RegularExpression(@"[0]{1}[0-9]{9}")]
        [MaxLength(10)]
        [MinLength(10)]
        public string? PhoneNumber { get; set; }

        public bool Gender { get; set; }
    }
}
