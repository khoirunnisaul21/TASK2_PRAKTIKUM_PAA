using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tugas2.Models;

namespace Tugas2.Controllers
{
    public class PersonController : Controller
    {
        private string __constr;
        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }

        //create
        [HttpPost("api/murid/create")]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.CreatePerson(person);
            return Ok("Person added successfully.");
        }

        //update
        [HttpPut("api/murid/update/{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person person)
        {
            person.id_person = id;
            PersonContext context = new PersonContext(this.__constr);
            context.UpdatePerson(id,person);
            return Ok("Person updated successfully.");
        }

        //delete
        [HttpDelete("api/murid/delete/{id}")]
        public IActionResult DeletePerson(int id)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.DeletePerson(id);
            return Ok("Person deleted successfully.");
        }

    }
}