using FinalExamSimulation.Models;

namespace FinalExamSimulation.Areas.Admin.ViewModels.Worker;

public class UpdateWorkerVm
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public int DesignationId { get; set; }
    public IFormFile? ImageFile { get; set; }

    public List<Designation> Designations { get; set; } = new List<Designation>();
}
