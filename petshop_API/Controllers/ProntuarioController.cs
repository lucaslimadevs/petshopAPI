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
    public class ProntuarioController : BasicController
    {
        private readonly ProntuarioRepository _repository;

        public ProntuarioController(INotification notification, ProntuarioRepository repository) : base(notification)
        {
            this._repository = repository;
        }

        [HttpGet]        
        public ActionResult<IEnumerable<Prontuario>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet]
        [Route("Filter")]
        public ActionResult<IEnumerable<Prontuario>> Filter([FromQuery] ProntuarioFilter filter)
        {
            return Ok(_repository.Filter(filter));
        }

        [HttpGet("{id}")]
        public ActionResult<Prontuario> Get(int id)
        {
            var prontuario = _repository.Get(id);
            if (prontuario != null)
                return Ok(prontuario);
            return NotFound();
        }

        [HttpPost]        
        public ActionResult<Prontuario> Insert([FromBody] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(prontuario);
                return CustomResponse(prontuario, "Prontuário cadastrado com sucesso");
            }

            return BadRequest();
        }

        [HttpPut]
        public ActionResult<Prontuario> Update([FromBody] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(prontuario);
                return CustomResponse(prontuario, "Prontuário atualizado com sucesso");
            }

            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Prontuario> Delete([FromBody] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(prontuario);
                return CustomResponse(prontuario, "Prontuário excluído com sucesso");
            }

            return BadRequest();
        }
    }
}
