using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> eventos = new Evento[] {
                new Evento(){
                    EventoId = 1,
                    Tema = "Angular e DotNet",
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString(),
                    ImagemURL = "imagem.jpg"
                },
                new Evento(){
                    EventoId = 2,
                    Tema = "DotNet e EntityFramework",
                    Local = "Recife",
                    Lote = "1º Lote",
                    QtdPessoas = 150,
                    DataEvento = DateTime.Now.AddDays(30).ToString(),
                    ImagemURL = "imagem.jpg"
                }
            };

        public EventoController() {}

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return eventos;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return eventos.Where(e => e.EventoId == id);
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
