using Domain.Infraestructure.Notification;
using Domain.Infraestructure.Controller;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.Entities;

namespace petshop_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlojamentoController : BasicController
    {
        private readonly AlojamentoRepository _repository;

        public AlojamentoController(INotification notification, AlojamentoRepository repository) : base(notification)
        {
            this._repository = repository;
        }

        [HttpGet]        
        public ActionResult<IEnumerable<Alojamento>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Alojamento> Get(int id)
        {
            var alojamento = _repository.Get(id);
            if (alojamento != null)
                return Ok(alojamento);
            return NotFound();
        }

        [HttpPost]        
        public ActionResult<Alojamento> Insert([FromBody] Alojamento alojamento)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(alojamento);
                return CustomResponse(alojamento, "Alojamento cadastrado com sucesso");
            }

            return BadRequest();
        }

        [HttpPut]
        public ActionResult<Alojamento> Update([FromBody] Alojamento alojamento)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(alojamento);
                return CustomResponse(alojamento, "Alojamento atualizado com sucesso");
            }

            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Alojamento> Delete([FromBody] Alojamento alojamento)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(alojamento);
                return CustomResponse(alojamento, "Alojamento excluído com sucesso");
            }

            return BadRequest();
        }
    }
}
