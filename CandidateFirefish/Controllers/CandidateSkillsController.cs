using CandidateFirefish.DTO;
using CandidateFirefish.SqlStatements;
using Microsoft.AspNetCore.Mvc;

namespace CandidateFirefish.Controllers
{
    /*
     * This is the candidate skills controller for retrieving the candidates and their skills
     * in this class you have a get, post and delete as specificed in the requirements
     */
    [Route("api/[Controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class CandidateSkillsController : ControllerBase
    {
        public readonly CandidateSkillSql _candidateSkills;

        public CandidateSkillsController()
        {
            _candidateSkills = new CandidateSkillSql();
        }
      

        [HttpGet]
        public IActionResult GetCandidateSkills()
        {
            
            var candidate = _candidateSkills.GetSkillCandidates();
            if (candidate is null)
                return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public IActionResult AddNewCandidateSkill([FromBody] CreateCandidateSkill model)
        {
            _candidateSkills.AddCandidateSkill(model);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCandidateSkill([FromBody] CreateCandidateSkill model)
        {
            _candidateSkills.DeleteSkillCandidate(model);
            return Ok();
        }


    }
}
