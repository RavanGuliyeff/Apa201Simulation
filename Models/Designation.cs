using FinalExamSimulation.Models.Base;
namespace FinalExamSimulation.Models;

public class Designation : BaseEntity
{
    public string Name { get; set; }

    //navigation property
    public List<Worker> Workers { get; set; } = new List<Worker>();
}
