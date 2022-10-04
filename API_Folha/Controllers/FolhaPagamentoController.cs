using System;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Folha;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/folha")]

    public class FolhaPagamentoController : ControllerBase
    {
        private readonly DataContext _context;

        public FolhaPagamentoController(DataContext context) => _context = context;

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] CadastrarFolha cadastrarFolha, int id)
        {
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f => f.Id.Equals(id));
            if (funcionario != null)
            {
                var folhapagamento = new FolhaPagamento
                {
                    funcionario = funcionario,
                    valorHora = cadastrarFolha.valorHora,
                    quantHoras = cadastrarFolha.quantHoras

                };

                _context.Folhas.Add(folhapagamento);
                _context.SaveChanges();
                return Created("", folhapagamento);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() => Ok(_context.Folhas.Include(x => x.funcionario).ToList());


        [HttpGet]
        [Route("buscar")]
        public IActionResult Buscar([FromRoute] string cpf, DateTime data)
        {
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf));
            if (cpf != null)
            {
                FolhaPagamento folhaPagamento = _context.Folhas.FirstOrDefault(f => f.CriadoEm.Equals(data));
                if (data != null)
                {
                    return Ok(folhaPagamento);
                }

                return NotFound("Data não encontrada");
            }

            return NotFound("CPF não encontrado");

        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            FolhaPagamento folhaPagamento = _context.Folhas.Find(id);
            if (folhaPagamento != null)
            {
                _context.Folhas.Remove(folhaPagamento);
                _context.SaveChanges();
                return Ok(folhaPagamento);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("filtrar")]
        public IActionResult Filtrar([FromRoute] DateTime data)
        {
            FolhaPagamento folhaPagamento = _context.Folhas.FirstOrDefault(f => f.CriadoEm.Equals(data));
            if (data != null)
            {
                return Ok(folhaPagamento);
            }
            else
            {
                return NotFound("Nehuma folha encontrada nessa data");

            }
        }


    }

}