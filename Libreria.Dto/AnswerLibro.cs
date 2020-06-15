using Libreria.DataAccess.DbMidels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libreria.Dto
{
    public class AnswerLibro
    {
        public int LibroId { get; set; }
        public string Titolo { get; set; }
        public DateTime AnnoPub { get; set; }
        public decimal Prezzo { get; set; }
        public int? Sconto { get; set; }
        public string NomeLibreria { get; set; }
        public string Luogo { get; set; }
        public List<AnswerAutore> Autori { get; set; }
        public static List<AnswerLibro> MappaPerLista(List<Libro> libri)
        {
            var res = new List<AnswerLibro>();
            foreach (var item in libri)
            {
                var tap = new AnswerLibro();
                tap.LibroId = item.LibroId;
                tap.Titolo = item.Titolo;
                tap.AnnoPub = item.AnnoPub;
                tap.Prezzo = item.Prezzo;
                tap.Sconto = item.Sconto;
                if(item.Libreria != null)
                {
                    tap.NomeLibreria = item.Libreria.NomeLibreria;
                    tap.Luogo = item.Libreria.Luogo;
                }
                tap.Autori = new List<AnswerAutore>();
                if (item.LibroAutores.Any())
                {
                    tap.Autori = item.LibroAutores.Select(x => new AnswerAutore
                    {
                        AutoreId = x.Autore.AutoreId,
                        Nome = x.Autore.Nome,
                        Cognome = x.Autore.Cognome
                    }).ToList();
                }
                res.Add(tap);
            }
            return res;
        }
    }
}
