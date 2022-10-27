using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos
{
       public class UserRegistrationRequestDto
        {
            [Required]
            public string name { get; set; }
            [Required]
            public string email { get; set; }
             [Required]
            public string password { get; set; }
        }
    
}
