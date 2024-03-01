using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAlmacen.Models;
using Microsoft.AspNetCore.Cors;

namespace ApiAlmacen.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaDetailController : ControllerBase
    {
        public readonly FacturaCamiloContext _dbcontext;

        public FacturaDetailController(FacturaCamiloContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaFacturaDetails")]
        public IActionResult Lista()
        {
            List<FacturaDetail> lista = new List<FacturaDetail>();

            try
            {
                lista = _dbcontext.FacturaDetails.Include(c => c.IdFacturaNavigation)
                           .Include(c => c.IdProductoNavigation)
                           .ToList();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


    }
}
