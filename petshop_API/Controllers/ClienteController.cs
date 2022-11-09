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
    public class ClienteController : BasicController
    {
        private readonly ClienteRepository _repository;

        public ClienteController(INotification notification, ClienteRepository repository) : base(notification)
        {
            this._repository = repository;
        }

        [HttpGet]        
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _repository.Get(id);
            if (cliente != null)
                return Ok(cliente);
            return NotFound();
        }

        [HttpPost]        
        public ActionResult<Cliente> Insert([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(cliente);
                return CustomResponse(cliente, "Cliente cadastrado com sucesso");
            }

            return BadRequest();
        }

        [HttpPut]
        public ActionResult<Cliente> Update([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(cliente);
                return CustomResponse(cliente, "Cliente atualizado com sucesso");
            }

            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Cliente> Delete([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(cliente);
                return CustomResponse(cliente, "Cliente excluído com sucesso");
            }

            return BadRequest();
        }
    }
}
