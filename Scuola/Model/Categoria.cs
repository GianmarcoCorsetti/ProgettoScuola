using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace Scuola.Model {
    public enum Category {
        GRAFICA,
        SVILUPPOSOFTWARE,
        LINGUE,
        SISTEMISTICA
    }
    public class Categoria {
        public long Id { get; set; }
        public Category CategoriaCorso { get; set; }
        public string Descrizione{ get; set; }
        public Categoria(long id, Category categoriaCorso, string descrizione)
        {
            Id = id;
            CategoriaCorso = categoriaCorso;
            Descrizione = descrizione;
        }
    }
}
