using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Todo.Client.Models;


public class RegisterModel
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]

    public string FirstName { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Last name can only contain letters")]

    public string LastName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required,MinLength(6)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$"
    , ErrorMessage = "Password must contain  uppercase ,lowercase and  numbers.")]
    public string Password { get; set; } = string.Empty;    

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set;} = string.Empty;
}
