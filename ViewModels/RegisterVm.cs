using System.ComponentModel.DataAnnotations;

namespace FinalExamSimulation.ViewModels;

public class RegisterVm
{
    [MinLength(3, ErrorMessage = "qisadir")]
    [MaxLength(40, ErrorMessage = "uzundur")]
    public string Fullname { get; set; }
    public string UserName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
