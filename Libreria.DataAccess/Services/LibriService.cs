using Libreria.DataAccess.DbMidels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Autore> CheckAutore(string nome, string cognome)
        {
            var res = await _libreriaContext.Autore.FirstOrDefaultAsync(x => x.Nome.Trim().ToLower() == nome.Trim().ToLower() && x.Cognome.Trim().ToLower() == cognome.Trim().ToLower());
            if (res != null )
            {
                return res;
            }
            else
            {
                var insert = new Autore();
                insert.Nome = nome;
                insert.Cognome = cognome;
                _libreriaContext.Autore.Add(insert);
                await _libreriaContext.SaveChangesAsync();
                return await _libreriaContext.Autore.FirstOrDefaultAsync(x => x.Nome == insert.Nome && x.Cognome == insert.Cognome);

            }
        }
        public async Task<int> AddLibroToDb(Libro libro) 
        {
            try
            {
                _libreriaContext.Libro.Add(libro);
                await _libreriaContext.SaveChangesAsync();
                var res =  await _libreriaContext.Libro.FirstOrDefaultAsync(x=>x.Titolo.Trim().ToLower() == libro.Titolo.ToLower().Trim());
                return res.LibroId;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<bool> UpdateLibro(Libro libro)
        {
            try
            {
                var ismod = false;
                var tomod = _libreriaContext.Libro.FirstOrDefault(x=>x.LibroId == libro.LibroId);
                if (tomod != null)
                {
                    if (tomod.Titolo != libro.Titolo)
                    {
                        tomod.Titolo = libro.Titolo;
                        ismod = true;
                    }
                    if (tomod.LibreriaId != libro.LibreriaId)
                    {
                        tomod.LibreriaId = libro.LibreriaId;
                        ismod = true;
                    }
                    if (tomod.AnnoPub != libro.AnnoPub)
                    {
                        tomod.AnnoPub = libro.AnnoPub;
                        ismod = true;
                    }
                    if (tomod.Prezzo != libro.Prezzo)
                    {
                        tomod.Prezzo = libro.Prezzo;
                        ismod = true;
                    }
                    if (tomod.Sconto != libro.Sconto)
                    {
                        tomod.Sconto = libro.Sconto;
                        ismod = true;
                    }
                    if (ismod)
                    {
                        _libreriaContext.Update(tomod);
                        await _libreriaContext.SaveChangesAsync();
                    }
                }
                return ismod;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
