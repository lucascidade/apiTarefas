using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Repositorios.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio; 
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodosUsuarios()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorId(int id)
        {
            TarefaModel tarefas = await _tarefaRepositorio.BuscarPorId(id);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefaModel) 
        {
            TarefaModel tarefa = await _tarefaRepositorio.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel usuario = await _tarefaRepositorio.Atualizar(tarefaModel, id);
            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(id);

            return Ok(apagado); 
        }
    }
}
