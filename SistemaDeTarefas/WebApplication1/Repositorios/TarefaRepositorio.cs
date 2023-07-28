using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios.Interfaces;
using System.Data.SqlTypes;
using WebApplication1.Models;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly Context _context;

        public TarefaRepositorio(Context context)
        {
            _context = context;
        }

        public async Task<TarefaModel> BuscarPorId(int id)
        {
            //meu context percorre a tabela Tarefa e pega o primeiro ou Default que encontrar desde que cumpra a expressão lambda
            return await _context.Tarefas.Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _context.Tarefas.Include(x => x.Usuario)
                .ToListAsync();
        }
        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
           await _context.Tarefas.AddAsync(tarefa);
           await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(id);
            if(tarefaPorId == null)
            {
                throw new Exception("Tarefa não encontrado! ");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _context.Tarefas.Update(tarefaPorId);
           await _context.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            TarefaModel tarefasPorId = await BuscarPorId(id);
            if(tarefasPorId==null)
            {
                throw new Exception("Tarefa nao encontrado! ");
            }
            _context.Tarefas.Remove(tarefasPorId);
            await _context.SaveChangesAsync();
            return true; 
            
        }
    }
}
