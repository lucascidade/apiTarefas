using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositorios.Interfaces;
using System.Data.SqlTypes;
using WebApplication1.Models;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Context _context;

        public UsuarioRepositorio(Context context)
        {
            _context = context;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            //meu context percorre a tabela USUARIO e pega o primeiro ou Default que encontrar desde que cumpra a expressão lambda
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
           await _context.Usuarios.AddAsync(usuario);
           await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            if(usuarioPorId == null)
            {
                throw new Exception("Usuario não encontrado! ");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            _context.Usuarios.Update(usuarioPorId);
           await _context.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            if(usuarioPorId==null)
            {
                throw new Exception("Usuario nao encontrado! ");
            }
            _context.Usuarios.Remove(usuarioPorId);
            await _context.SaveChangesAsync();
            return true; 
            
        }
    }
}
