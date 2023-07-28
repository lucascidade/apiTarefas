using WebApplication1.Models;

namespace SistemaDeTarefas.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BuscarTodasTarefas();
        Task<TarefaModel> BuscarPorId(int id);
        Task<TarefaModel> Adicionar(TarefaModel usuario);
        Task<TarefaModel> Atualizar(TarefaModel usuario, int id);
        Task<bool> Apagar(int id);

    }
}
