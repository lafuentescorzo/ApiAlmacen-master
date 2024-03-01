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
    public class ProductoController : ControllerBase
    {
        public readonly FacturaCamiloContext _dbcontext;

        public ProductoController(FacturaCamiloContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaProducto")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }


    }
}
