using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Finanziatore {
        public long Id{ get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Finanziatore(long id, string titolo, string descrizione)
        {
            Id = id;
            Titolo = titolo;
            Descrizione = descrizione;
        }
    }
}
