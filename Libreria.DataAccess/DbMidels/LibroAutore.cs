using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.DataAccess.DbMidels
{
    public class LibroAutore
    {
        public int LibroAutoreId { get; set; }
        public int  LibroId{ get; set; }
        public int AutoreId { get; set; }
        public virtual Libro Libro { get; set; }
        public virtual Autore Autore { get; set; }
    }
}
