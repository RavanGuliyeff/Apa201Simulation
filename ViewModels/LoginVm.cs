using System.ComponentModel.DataAnnotations;

namespace FinalExamSimulation.ViewModels;

public class LoginVm
{
    public string UserNameOrEmail { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool IsPersisted { get; set; }
}
