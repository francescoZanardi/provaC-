using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.DataAccess.DbMidels;

namespace Libreria.DataAccess.Services
{
    public interface ILibriService
    {
        Task<List<Libro>> GetLibro();
    }
}