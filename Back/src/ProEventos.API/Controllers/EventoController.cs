using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {        
        private readonly ProEventosContext _context;

        public EventoController(ProEventosContext context)
        {
            this._context = context;
        }            

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(e => e.Id == id);

            return evento;
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return "Exemplo Put";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "Exemplo Delete";
        }
    }
}
