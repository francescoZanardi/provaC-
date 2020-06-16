using System.Threading.Tasks;
using Libreria.Dto;

namespace Libreria.Core
{
    public interface ILibroCore
    {
        Task<int> PostLibro(RequestLibro requestLibro);
    }
}