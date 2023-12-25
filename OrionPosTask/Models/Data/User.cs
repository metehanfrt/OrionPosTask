using System.ComponentModel.DataAnnotations;

namespace OrionPosTask.Models.Data
{
    public class User : IBaseEntity
    {
        
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Geçersiz Şifre veya hatalı giriş.")]
        [MaxLength(100)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Geçersiz Eposta veya hatalı giriş.")]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }
    }

}
