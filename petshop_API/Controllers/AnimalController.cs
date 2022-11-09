using Domain.Infraestructure.Notification;
using Domain.Infraestructure.Controller;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.Filter;

namespace petshop_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimalController : BasicController
    {
        private readonly AnimalRepository _repository;

        public AnimalController(INotification notification, AnimalRepository repository) : base(notification)
        {
            this._repository = repository;
        }

        [HttpGet]        
        public ActionResult<IEnumerable<Animal>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet]
        [Route("Filter")]
        public ActionResult<IEnumerable<Animal>> Filter([FromQuery] AnimalFilter filter)
        {
            return Ok(_repository.Filter(filter));
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id)
        {
            var animal = _repository.Get(id);
            if (animal != null)
                return Ok(animal);
            return NotFound();
        }

        [HttpPost]        
        public ActionResult<Animal> Insert([FromBody] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(animal);
                return CustomResponse(animal, "Animal cadastrado com sucesso");
            }

            return BadRequest();
        }

        [HttpPut]
        public ActionResult<Animal> Update([FromBody] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(animal);
                return CustomResponse(animal, "Animal atualizado com sucesso");
            }

            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Animal> Delete([FromBody] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(animal);
                return CustomResponse(animal, "Animal excluído com sucesso");
            }

            return BadRequest();
        }
    }
}
