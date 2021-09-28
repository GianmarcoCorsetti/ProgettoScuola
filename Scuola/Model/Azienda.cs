using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Azienda {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Citta { get; set; }
        public string Indirizzo { get; set; }
        public string CP { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string PartitaIva { get; set; }
        public Azienda(long id, string nome, string citta, string indirizzo, string cP, string telefono, string email, string partitaIva)
        {
            Id = id;
            Nome = nome;
            Citta = citta;
            Indirizzo = indirizzo;
            CP = cP;
            Telefono = telefono;
            Email = email;
            PartitaIva = partitaIva;
        }
    }
}
