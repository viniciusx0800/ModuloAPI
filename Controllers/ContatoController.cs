using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        private object contato;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato) 
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id }, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return Ok(contato);
        }
        
        [HttpGet("Obter Contato por Nome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }



        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var  contatoBanco = _context.Contatos.Find(id);
            
            if (contatoBanco == null)
                return NotFound();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var deletarContato = _context.Contatos.Find(id);

            if(deletarContato == null)
                return NotFound();

            // _context.Contatos.Remove(deletarContato);
            // _context.SaveChanges();
            // return NoContent();

            deletarContato.Ativo = false;

            _context.Contatos.Update(deletarContato);
            _context.SaveChanges();

            return Ok("Deletado com sucesso!");

        }
    }
}