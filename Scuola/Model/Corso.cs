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
        public Level Livello { get; set; }
        public Progetti Project { get; set; }
        public Categoria Category { get; set; }
        public Corso(long id, string titolo, int ammontareOre, string descrizione, decimal costoDiRiferimento, Level livello, Progetti project, Categoria category)
        {
            Id = id;
            Titolo = titolo;
            AmmontareOre = ammontareOre;
            Descrizione = descrizione;
            CostoDiRiferimento = costoDiRiferimento;
            Livello = livello;
            Project = project;
            Category = category;
        }
        public override string ToString()
        {
            return $"ID: {Id} Titolo: {Titolo} Livello: {Livello.LivelloCorso}";
        }
    }
}
