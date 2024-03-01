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
    public class FacturaController : ControllerBase
    {
        public readonly FacturaCamiloContext _dbcontext;

        public FacturaController(FacturaCamiloContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("listaFacturas")]
        public IActionResult Lista()
        {
            List<Factura> lista = new List<Factura>();

            try
            {
                lista = _dbcontext.Facturas.Include(c => c.CcClienteNavigation)
                           .Include(c => c.CcVendedorNavigation)
                           .ToList();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("obtener/{IdFactura:int}")]
        public IActionResult Obtener(int IdFactura)
        {
            Factura factura = _dbcontext.Facturas.Find(IdFactura);

            if(factura == null)
            {
                BadRequest("Factura no encontrada");
            }

            try
            {
                factura = _dbcontext.Facturas.Include(c => c.CcClienteNavigation)
                    .Include(c => c.CcVendedorNavigation)
                    .Where(c => c.IdFactura == IdFactura).FirstOrDefault();

             return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = factura });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = factura });
            }

        }

        [HttpPost]
        [Route("guardadFactura")]
        public IActionResult Guardar([FromBody] Factura factura)
        {
            try
            {
                var cliente = _dbcontext.Clientes.Find(factura.CcCliente);
                if (cliente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el cliente asociado a la factura" });
                }

                // Cargar el objeto Vendedor relacionado
                var vendedor = _dbcontext.Vendedors.Find(factura.CcVendedor);
                if (vendedor == null)
                {
                    return NotFound(new { mensaje = "No se encontró el vendedor asociado a la factura" });
                }

                // Asignar las entidades relacionadas a las propiedades de navegación
                factura.CcClienteNavigation = cliente;
                factura.CcVendedorNavigation = vendedor;

                // Agregar la factura al contexto y guardar los cambios
                _dbcontext.Facturas.Add(factura);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }

        [HttpPut]
        [Route("editarFactura")]
        public IActionResult Editar([FromBody] Factura factura)
        {
            try
            {

                Factura objeto = _dbcontext.Facturas.Find(factura.IdFactura);
                var cliente = _dbcontext.Clientes.Find(factura.CcCliente);
                var vendedor = _dbcontext.Vendedors.Find(factura.CcVendedor);
                if (cliente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el cliente asociado a la factura" });
                }
                if (vendedor == null)
                {
                    return NotFound(new { mensaje = "No se encontró el vendedor asociado a la factura" });
                }

                if (objeto == null)
                {
                    BadRequest("Factura no encontrada");
                }
               

                objeto.CcVendedor = factura.CcVendedor is 0 ? objeto.CcVendedor : factura.CcVendedor;
                objeto.CcCliente = factura.CcCliente is 0 ? objeto.CcCliente : factura.CcCliente;
                objeto.FechaVenta = factura.FechaVenta != DateTime.MinValue ? objeto.FechaVenta : factura.FechaVenta;
                objeto.Total = factura.Total is 0 ? objeto.Total : factura.Total;

                _dbcontext.Facturas.Update(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }

        [HttpDelete]
        [Route("eliminar/{IdFactura:int}")]
        public IActionResult Eliminar(int IdFactura)
        {
            Factura factura = _dbcontext.Facturas.Find(IdFactura);

            if (factura == null)
            {
                BadRequest("Factura no encontrada");
            }

            try
            {
                _dbcontext.Facturas.Remove(factura);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = factura });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = factura });
            }

        }

    }
}
