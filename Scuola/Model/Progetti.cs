using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Progetti {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Azienda ClassAzienda { get; set; }
        public Progetti(long id, string titolo, string descrizione, Azienda classAzienda)
        {
            Id = id;
            Titolo = titolo;
            Descrizione = descrizione;
            ClassAzienda = classAzienda;
        }
    }
}
