using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {

    public enum ExperienceLevel{ // convenzione si usa una notazione tutta maiuscola
        PRINCIPIANTE,
        MEDIO,
        ESPERTO,
        GURU
    }
    public class Corso : Object{
        public long Id { get; set; }
        public string Titolo { get; set; }
        public int AmmontareOre { get; set; }
        public ExperienceLevel Level { get; set; }
        public int MyProperty { get; set; }
        public string Descrizione { get; set; }
        public decimal CostoDiRiferimento { get; set; }

        public Corso(long newId, string newTitolo, int newDurataInOre, ExperienceLevel newLevel, string newDescription, decimal newStandardPrice){
            Id = newId;
            Titolo = newTitolo;
            DurataInOre = newDurataInOre;
            Description = newDescription;
            StandardPrice = newStandardPrice;
        }
        public override string ToString()
        {
            return $"ID: {Id} Titolo: {Titolo} Livello: {Level}";
        }
    }
}
