using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica.Interfaces;
using prueba_tecnica.Models;

namespace prueba_tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuario;

        public UsuarioController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            return Ok(await _usuario.GetAllUsuario());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            return Ok(await _usuario.GetUsuario(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _usuario.InsertUsuario(usuario);

            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usuario.UpdateUsuario(usuario);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuario.DeleteUsuario(new Usuario { Id = id });

            return NoContent();
        }
    }
}
