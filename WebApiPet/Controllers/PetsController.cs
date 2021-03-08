using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        // GET: api/<PetsController>
        [HttpGet]
        public string Get()
        {
            return "Verbo GET sin parámetro";
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}/{nombre}")]
        public string Get(int id, string nombre)
        {
            return $"Hola {nombre}, estas ejecutando el verbo GET con parámetro {id}";
        }

        // POST api/<PetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
