﻿using Libreria.DataAccess.DbMidels;
using Libreria.DataAccess.Services;
using Libreria.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libreria.Core
{
    public class LibroCore : ILibroCore
    {
        private readonly ILibriService _libriService;
        public LibroCore(ILibriService libriService)
        {
            _libriService = libriService;
        }
        public async Task<RequestLibro> PostLibro(RequestLibro requestLibro)
        {
            var esisteLibro = _libriService.GetLibroByTitolo(requestLibro.Titolo);
            if (esisteLibro == null)
            {
                var DaInserire = new Libro();
                DaInserire.Titolo = requestLibro.Titolo;
                DaInserire.AnnoPub = requestLibro.AnnoPub;
                DaInserire.Prezzo = requestLibro.Prezzo;
                DaInserire.Sconto = requestLibro.Sconto;
                DaInserire.Libreria = await _libriService.Check(requestLibro.NomeLibreria, requestLibro.Luogo);
                DaInserire.LibroAutores = new List<LibroAutore>();
                var tmp = new LibroAutore();
                tmp.Autore = await _libriService.CheckAutore(requestLibro.NomeAutore, requestLibro.CognomeAutore);
                tmp.Libro = DaInserire;
                DaInserire.LibroAutores.Add(tmp);
                var insert = await _libriService.AddLibroToDb(DaInserire);
                if (insert)
                {
                    return requestLibro;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
