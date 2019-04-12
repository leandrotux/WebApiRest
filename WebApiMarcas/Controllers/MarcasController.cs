using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiMarcas.Models;

namespace WebApiMarcas.Controllers
{
    public class MarcasController : ApiController
    {
        private DbMarcaContext dbMarca = new DbMarcaContext();

        public IHttpActionResult PostMarca(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbMarca.Marcas.Add(marca);
            dbMarca.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = marca.Id }, marca);
        }

        public IHttpActionResult GetMarca(int id)
        {
            var marca = dbMarca.Marcas.Find(id);

            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        //public IHttpActionResult GetMarca()
        //{
        //    var marca = dbMarca.Marcas;

        //    if (marca == null)
        //        return NotFound();

        //    return Ok(marca);
        //}

        public IHttpActionResult PutMarca(int id, Marca marca)
        {

            dbMarca.Entry(marca).State = System.Data.Entity.EntityState.Modified;
            dbMarca.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteMarca(int id)
        {
            var marca = dbMarca.Marcas.Find(id);
            dbMarca.Marcas.Remove(marca);
            dbMarca.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult GetMarca(int pagina = 1, int quantidadeRegistros = 10)
        {
            if (pagina <= 0 || quantidadeRegistros <= 0)
            {
                return BadRequest("Os parâmetros pagina e quantidadeRegistros não podem ser menores que zero.");
            }
            if (quantidadeRegistros > 10)
            {
                return BadRequest("O máximo de paginas permitido é 10.");
            }

            int totalPaginas = (int)Math.Ceiling(dbMarca.Marcas.Count() / Convert.ToDecimal(quantidadeRegistros));

            if (pagina > totalPaginas)
            {
                return BadRequest("A página solicitada não existe.");
            }
            
            if (pagina > 1) System.Web.HttpContext.Current.Response.AddHeader("X-Pages-PreviousPage", Url.Link("DefaultApi", new { pagina = pagina - 1, quantidadeRegistros = quantidadeRegistros }));

            if (pagina < totalPaginas)System.Web.HttpContext.Current.Response.AddHeader("X-Pages-NextPage", Url.Link("DefaultApi", new { pagina = pagina + 1, quantidadeRegistros = quantidadeRegistros }));

            System.Web.HttpContext.Current.Response.AddHeader("X-Pages-TotalPages", totalPaginas.ToString());
            var marca = dbMarca.Marcas.OrderBy(c => c.Id).Skip(quantidadeRegistros * (pagina - 1)).Take(quantidadeRegistros);
            return Ok(marca);
        }

    }
}
