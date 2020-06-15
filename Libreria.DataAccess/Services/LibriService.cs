using Libreria.DataAccess.DbMidels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.DataAccess.Services
{
    public class LibriService : ILibriService
    {
        private readonly LibreriaContext _libreriaContext;
        public LibriService(LibreriaContext libreriaContext)
        {
            _libreriaContext = libreriaContext;
        }
        public async Task<List<Libro>> GetLibri()
        {
            try
            {
                var result = await _libreriaContext.Libro
                    .Include(x => x.Libreria)
                    .Include(x => x.LibroAutores)
                    .ThenInclude(x => x.Autore).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Libro>GetLibro(int id)
        {
            try
            {
                var res = await _libreriaContext.Libro
                    .Include(x => x.Libreria)
                    .Include(x => x.LibroAutores)
                    .ThenInclude(x => x.Autore).FirstOrDefaultAsync(x=> x.LibroId == id);
                if (res != null)
                {
                    return res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Libro>GetLibroByTitolo(string titolo)
        {
            return await _libreriaContext.Libro.FirstOrDefaultAsync(x=>x.Titolo.Trim().ToLower() == titolo);
        }

       public async Task<Libreriaa> Check(string nome, string luogo) {

            var res = await _libreriaContext.Libreriaa
                .FirstOrDefaultAsync(x=>x.NomeLibreria.Trim().ToLower() == nome.Trim().ToLower()
                && x.Luogo.Trim().ToLower() == luogo.Trim().ToLower());
            if (res != null)
            {
                return res;
            }
            else
            {
                var insert = new Libreriaa();
                insert.NomeLibreria = nome;
                insert.Luogo = luogo;
               _libreriaContext.Libreriaa.Add(insert);
                await _libreriaContext.SaveChangesAsync();
                return await _libreriaContext.Libreriaa.FirstOrDefaultAsync(x=>x.NomeLibreria == insert.NomeLibreria && x.Luogo == insert.Luogo);

            }
        }
    }
}
