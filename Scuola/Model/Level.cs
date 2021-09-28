using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public enum ExperienceLevel { // convenzione si usa una notazione tutta maiuscola
        PRINCIPIANTE,
        MEDIO,
        ESPERTO,
        GURU
    }
    public class Level {
        
        public long Id { get; set; }
        public ExperienceLevel LivelloCorso { get; set; }
        public string Descrizione { get; set; }
        public Level(long id, ExperienceLevel livelloCorso, string descrizione)
        {
            Id = id;
            LivelloCorso = livelloCorso;
            Descrizione = descrizione;
        }
    }
}
