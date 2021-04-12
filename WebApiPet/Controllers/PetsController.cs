using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public PetsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: <PetsController>
        [HttpGet]
        public List<PetModel> Get()
        {
            return new PetModel().GetAll(Configuration.GetConnectionString("MySQL"));
        }

        // GET <PetsController>/5
        [HttpGet("{id}")]
        public PetModel Get(int id)
        {
            return new PetModel().Get(Configuration.GetConnectionString("MySQL"), id);
        }

        // POST <PetsController>
        [HttpPost]
        public ApiResponse Post([FromBody] PetModel pet)
        {
            return pet.Insert(Configuration.GetConnectionString("MySQL"));
        }

        // PUT <PetsController>/5
        [HttpPut]
        public ApiResponse Put([FromBody] PetModel pet)
        {
            return pet.Update(Configuration.GetConnectionString("MySQL"));
        }

        // DELETE <PetsController>/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new PetModel().Delete(Configuration.GetConnectionString("MySQL"), id);
        }
    }
}
