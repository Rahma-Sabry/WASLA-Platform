namespace Wasla.Models
{
    public class EmployeeSkill
    {
        public int? YOE { get; set; }
        public int EmployeeId { get; set; }
        public int? SkillId {  get; set; }
        public Employee? Employee { get; set; }
        public Skill? Skill { get; set; }
    }
}
