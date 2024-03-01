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
    public class VendedorController : ControllerBase
    {
        public readonly FacturaCamiloContext _dbcontext;

        public VendedorController(FacturaCamiloContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaVendedores")]
        public IActionResult Lista()
        {
            List<Vendedor> lista = new List<Vendedor>();

            try
            {
                lista = _dbcontext.Vendedors.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


    }
}
