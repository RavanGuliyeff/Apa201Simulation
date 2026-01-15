using FinalExamSimulation.Models.Base;

namespace FinalExamSimulation.Models;

public class Worker : BaseEntity
{
    public string Fullname { get; set; }
    public string ImageUrl { get; set; }


    //relation property
    public int DesignationId { get; set; }
    public Designation Designation { get; set; }
}
