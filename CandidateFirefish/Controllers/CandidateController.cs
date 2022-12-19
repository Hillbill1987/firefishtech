using CandidateFirefish.DTo;
using CandidateFirefish.SqlStatements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace CandidateFirefish.Controllers
{
    /*
     * this is the candidate controller to send a call to fer the information from the db
     */
    [Route("api/[Controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class CandidateController : Controller
    {
        public readonly CandidateSql _candidateFirefish;
       
        public CandidateController()
        {
            _candidateFirefish = new CandidateSql();
            
        }

        [HttpGet]
        [Route("/api/Candidate/GetAllCandidates")]
        public JsonResult GetAllCandidates()
        {
            
            if (_candidateFirefish.GetAllCandidates().Count() > 0)
            {
                return new JsonResult(_candidateFirefish.GetAllCandidates());
            }
            return new JsonResult("");
        }



        [HttpPost]
        //[Route("Candidate/AddNewCandidates")]
        public IActionResult AddNewCandidates([FromBody]Candidate model)
        {
            _candidateFirefish.AddNewCandidate(model);      
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCandidates([FromBody] Candidate model)
        {
            _candidateFirefish.UpdateCandidate(model);          
            return Ok();
        }

      


    }
}


