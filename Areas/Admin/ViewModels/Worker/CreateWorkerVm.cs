using FinalExamSimulation.Models;

namespace FinalExamSimulation.Areas.Admin.ViewModels.Worker;

public class CreateWorkerVm
{
    public string Fullname { get; set; }
    public int DesignationId { get; set; }
    public IFormFile ImageFile { get; set; }
    public List<Designation> Designations { get; set; } = new List<Designation>();
}
