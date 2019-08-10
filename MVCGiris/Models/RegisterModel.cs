using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DisplayName("Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [DisplayName("Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare(nameof(Sifre),ErrorMessage = "{0} ile {1} eşleşmiyor")]
        public string SifreTekrar { get; set; }

    }
}