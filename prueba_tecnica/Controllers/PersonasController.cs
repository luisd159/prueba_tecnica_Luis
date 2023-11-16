using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica.Interfaces;
using prueba_tecnica.Models;

namespace prueba_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasRepository _persona;

        public PersonasController(IPersonasRepository personas)
        {
            _persona = personas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonas()
        {
            return Ok(await _persona.GetAllPersonas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            return Ok(await _persona.GetPersona(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] Personas personas)
        {
            if(personas == null)
                return BadRequest();
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _persona.InsertPersona(personas);

            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersona([FromBody] Personas personas)
        {
            if (personas == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _persona.UpdatePersona(personas);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await _persona.DeletePersona(new Personas { Identificador = id });

            return NoContent();
        }

    }
}
