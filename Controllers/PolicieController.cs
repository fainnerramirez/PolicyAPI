using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Policies.Models;
using Policies.Services;

namespace Policies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicieController: ControllerBase
    {
        private readonly IPoliciesServices _policieService;
        private PoliciesSettings context;

        public PolicieController(IPoliciesServices policieService) {
            this._policieService = policieService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Policie>> Get()
        {
            return _policieService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Policie> Get(string id) {

            var policie = _policieService.Get(id);

            if(policie == null)
            {
                return NotFound($"La poliza con id {id} no existe.");
            }

            return Ok(policie);
        }

        [HttpGet("vehicle/{plate}")]
        [Authorize]
        public ActionResult<Policie> GetPoliciePlaca(string plate)
        {
            var policie = _policieService.GetPoliciePlate(plate);

            if (policie == null)
            {
                return NotFound($"La poliza con placa {plate} no existe.");
            }

            return Ok(policie);
        }

        [HttpGet("{numberPolicy}")]
        [Authorize]
        public ActionResult<Policie> GetPolicieNumber(int numberPolicy)
        {
            var numberpolicie = _policieService.GetNumberPolicie(numberPolicy);

            if (numberpolicie == null)
            {
                return NotFound($"La poliza con numero de poliza {numberPolicy} no existe.");
            }

            return Ok(numberpolicie);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Policie> CreatePolicy([FromBody] Policie policy)
        {
            if (policy.FechaInicioVigencia > DateTime.Now || policy.FechaFinVigencia < DateTime.Now)
            {
                return BadRequest("La póliza no está vigente.");
            }

            _policieService.Create(policy);

            return CreatedAtAction(nameof(Get), new { id = policy.Id }, policy);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdatePolicy(string id, [FromBody] Policie policy)
        {
            var existingpolicie = _policieService.Get(id);

            if (existingpolicie == null)
            {
                return NotFound($"La poliza con id {id} no existe.");
            }

            _policieService.Update(id, policy);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeletePolicy(string id)
        {
            var existingpolicie = _policieService.Get(id);

            if (existingpolicie == null)
            {
                return NotFound($"La poliza con id {id} no existe.");
            }

            _policieService.Delete(id);

            return Ok($"La poliza con id {id} ha sido eliminada correctamente");
        }
    }
}