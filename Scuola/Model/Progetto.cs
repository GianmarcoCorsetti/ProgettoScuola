using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Progetto {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public Azienda Azienda { get; set; }
        public long IdAzienda{ get; set; }
        public Progetto(long id, string titolo, string descrizione, long idAzienda)
        {
            Id = id;
            Titolo = titolo;
            Descrizione = descrizione;
            IdAzienda = idAzienda;
        }
        public Progetto(long id, string titolo, string descrizione, Azienda azienda)
        {
            Id = id;
            Titolo = titolo;
            Descrizione = descrizione;
            Azienda = azienda;
        }

    }
}
