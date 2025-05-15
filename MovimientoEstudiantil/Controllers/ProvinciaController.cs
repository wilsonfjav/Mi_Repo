using Microsoft.AspNetCore.Mvc;
using MovimientoEstudiantil.Data;
using MovimientoEstudiantil.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovimientoEstudiantil.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinciaController : ControllerBase
    {
        private readonly MovimientoEstudiantilContext _context;

        public ProvinciaController(MovimientoEstudiantilContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Provincia>> ListaProvincias()
        {
            var provincias = _context.Provincias.ToList();
            return Ok(provincias);
        }
    }
}
