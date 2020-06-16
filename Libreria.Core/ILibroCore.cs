using System.Threading.Tasks;
using Libreria.Dto;

namespace Libreria.Core
{
    public interface ILibroCore
    {
        Task<RequestLibro> PostLibro(RequestLibro requestLibro);
    }
}