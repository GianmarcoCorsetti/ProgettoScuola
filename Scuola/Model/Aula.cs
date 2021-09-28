using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Aula {
        public long Id{ get; set; }
        public string Nome{ get; set; }
        public int CapacitaMax { get; set; }
        public bool Virtuale{ get; set; }
        public bool IsComputerized { get; set; }
        public bool HasProjector { get; set; }
        public Aula(long id, string nome, int capacitaMax, bool virtuale, bool isComputerized, bool hasProjector)
        {
            Id = id;
            Nome = nome;
            CapacitaMax = capacitaMax;
            Virtuale = virtuale;
            IsComputerized = isComputerized;
            HasProjector = hasProjector;
        }
    }
}
