using System.ComponentModel.DataAnnotations;

namespace BaseProjectAPI.Models
{
				public class UserSignInDTO
				{
								[Required, EmailAddress]
								public string Email { get; set; } = null!;
								[Required]
								public string Password { get; set; } = null!;
				}
}
