using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioBemol.Models;

namespace DesafioBemol.Repositories
{
    public interface IObjetoRepository
    {
        Task<Objeto> GetObjetoById(string id);
        Task<IEnumerable<Objeto>> GetAllObjetos();
        Task CreateObjeto(Objeto objeto);
        Task UpdateObjeto(Objeto objeto);
        Task DeleteObjeto(string id);
    }
}