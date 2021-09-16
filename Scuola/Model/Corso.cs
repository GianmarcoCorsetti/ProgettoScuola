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
    public class Corso {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public int DurataInOre { get; set; }
        public ExperienceLevel Level { get; set; }
        public string Description { get; set; }
        public decimal StandardPrice { get; set; }

        public Corso(long newId, string newTitolo, int newDurataInOre, ExperienceLevel newLevel, string newDescription, decimal newStandardPrice){
            Id = newId;
            Titolo = newTitolo;
            DurataInOre = newDurataInOre;
            Description = newDescription;
            StandardPrice = newStandardPrice;
        }
    }
}
