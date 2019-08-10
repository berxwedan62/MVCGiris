using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class UpdatePwModel
    {
        [DataType(DataType.Password)]
        public string OldPw { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Pw { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare(nameof(Pw), ErrorMessage = "{0} ile {1} eşleşmiyor")]
        public string RePw { get; set; }
    }
}