using System;
using System.Threading.Tasks;

namespace MeuProjeto.Repositories
{
    public interface IObjetoRepository
    {
        Task ProcessarObjeto(Objeto objeto);
    }
}
