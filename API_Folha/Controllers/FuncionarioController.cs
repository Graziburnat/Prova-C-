using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly DataContext _context;
        public FuncionarioController(DataContext context) =>
            _context = context;

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() => Ok(_context.Funcionarios.ToList());

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return Created("", funcionario);
        }

        [HttpGet]
        [Route("buscar/{cpf}")]
        public IActionResult Buscar([FromRoute] string cpf)
        {
            Funcionario funcionario = _context.Funcionarios.
                FirstOrDefault(f => f.Cpf.Equals(cpf));
            return funcionario != null ? Ok(funcionario) : NotFound();
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            Funcionario funcionario = _context.Funcionarios.Find(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
                _context.SaveChanges();
                return Ok(funcionario);
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("alterar")]
        public IActionResult Alterar([FromBody] Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Update(funcionario);
                _context.SaveChanges();
                return Ok(funcionario);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}