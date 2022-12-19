using System.ComponentModel.DataAnnotations;

namespace CandidateFirefish.DTO
{
    /*
     * this is the candidate skill model
     */
    public class CandidateSkill
    {
        [Key]
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string SkillName { get; set; }
    }
}
