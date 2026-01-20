using Microsoft.AspNetCore.Identity;

namespace FinalExamSimulation.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
}
