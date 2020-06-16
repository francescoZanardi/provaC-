using System.Collections.Generic;
using System.Threading.Tasks;
using Libreria.DataAccess.DbMidels;

namespace Libreria.DataAccess.Services
{
    public interface ILibriService
    {
        Task<List<Libro>> GetLibri();
        Task<Libro> GetLibro(int id);
        Task<Libro> GetLibroByTitolo(string titolo);
        Task<Libreriaa> Check(string nome, string luogo);
        Task<Autore> CheckAutore(string nome, string cognome);
        Task<int> AddLibroToDb(Libro libro);
    }
}