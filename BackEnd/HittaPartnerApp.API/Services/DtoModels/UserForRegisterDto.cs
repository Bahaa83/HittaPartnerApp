using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Services.DtoModels
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [StringLength(10,MinimumLength =4,ErrorMessage ="Låsenördet får inte bestå av mer än tiotecken och inte mindre än fyra")]
        [Required]
        public string Password { get; set; }
    }
}
