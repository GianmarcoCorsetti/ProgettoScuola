using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Corso {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public int AmmontareOre { get; set; }
        public string Descrizione { get; set; }
        public decimal CostoDiRiferimento { get; set; }
        public Livello Livello { get; set; }
        public long IdLivello { get; set; }
        public Progetto Progetto { get; set; }
        public long IdProgetto { get; set; }
        public Categoria Categoria { get; set; }
        public long IdCategoria { get; set; }
        public Corso(long id, string titolo, int ammontareOre, string descrizione, decimal costoDiRiferimento, long idLivello, long idProgetto, long idCategoria)
        {
            Id = id;
            Titolo = titolo;
            AmmontareOre = ammontareOre;
            Descrizione = descrizione;
            CostoDiRiferimento = costoDiRiferimento;
            IdLivello = idLivello;
            IdProgetto = idProgetto;
            IdCategoria = idCategoria;
        }
        public Corso(long id, string titolo, int ammontareOre, string descrizione, decimal costoDiRiferimento, Livello livello, Progetto progetto, Categoria categoria)
        {
            Id = id;
            Titolo = titolo;
            AmmontareOre = ammontareOre;
            Descrizione = descrizione;
            CostoDiRiferimento = costoDiRiferimento;
            Livello = livello;
            Progetto = progetto;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"ID: {Id} Titolo: {Titolo}";
        }
    }
}
