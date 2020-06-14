using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libreria.DataAccess.DbMidels
{
     public class Libreriaa
     {
        [Key]
        public int LibreriaId { get; set; }
        public string NomeLibreria { get; set; }
        public string Luogo { get; set; }
        public virtual  Libro Libro{ get; set; }
    }
}
