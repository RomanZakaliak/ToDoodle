using System.ComponentModel.DataAnnotations;

namespace ToDoodleBlazor.DTOs
{
    public class RegisterDTO : LoginDTO // TODO: Replace inheritance with composition
    {
        [Required]
        public string Name { get; set; } = string.Empty;


        [Required, Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
