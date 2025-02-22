using System.ComponentModel.DataAnnotations;

namespace Todo.Client.Models;

public class UpdateUserModel
{

    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Last name can only contain letters")]

    public string LastName { get; set; } = string.Empty;

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"
    , ErrorMessage = "Password must contain  uppercase ,lowercase and  numbers.")]
    public string PasswordHash { get; set; }
    [Compare("PasswordHash")]
    public string ConfirmPassword { get; set; }
};
