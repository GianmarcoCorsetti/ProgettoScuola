using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class EdizioneCorso{
        public long Id { get; set; }
        public Corso NomeCorso { get; set; }
        public LocalDate Start { get; set; }
        public LocalDate End{ get; set; }
        public int NumStudents { get; set; }
        public decimal RealPrice { get; set; }

    }
}
