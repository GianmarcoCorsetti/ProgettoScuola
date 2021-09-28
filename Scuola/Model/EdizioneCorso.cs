using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class EdizioneCorso{
        public long Id { get; set; }
        public string CodiceEdizione { get; set; }
        public LocalDate Start { get; set; }
        public LocalDate End{ get; set; }
        public int MaxStudenti { get; set; }
        public decimal RealPrice { get; set; }
        public bool InPresenza{ get; set; }
        public Aula Aula { get; set; }
        public long IdAula{ get; set; }
        public Corso Corso { get; set; }
        public long IdCorso { get; set; }
        public Finanziatore Finanziatore { get; set; }
        public long IdFinanziatore{ get; set; }
        public EdizioneCorso(long id, string codiceEdizione, LocalDate start, LocalDate end, int maxStudenti, decimal realPrice, bool inPresenza, long idAula, long idCorso, long idFinanziatore)
        {
            Id = id;
            CodiceEdizione = codiceEdizione;
            Start = start;
            End = end;
            MaxStudenti = maxStudenti;
            RealPrice = realPrice;
            InPresenza = inPresenza;
            IdAula = idAula;
            IdCorso = idCorso;
            IdFinanziatore = idFinanziatore;
        }
        public EdizioneCorso(long id, string codiceEdizione, LocalDate start, LocalDate end, int maxStudenti, decimal realPrice, bool inPresenza, Aula aula, Corso corso, Finanziatore finanziatore)
        {
            Id = id;
            CodiceEdizione = codiceEdizione;
            Start = start;
            End = end;
            MaxStudenti = maxStudenti;
            RealPrice = realPrice;
            InPresenza = inPresenza;
            Aula = aula;
            Corso = corso;
            Finanziatore = finanziatore;
        }

        public EdizioneCorso(int id, LocalDate start, LocalDate end, int maxStudenti, decimal realPrice, bool inPresenza, Aula aula, Corso corso, Finanziatore finanziatore)
        {
            Id = id;
            Start = start;
            End = end;
            MaxStudenti = maxStudenti;
            RealPrice = realPrice;
            InPresenza = inPresenza;
            Aula = aula;
            Corso = corso;
            Finanziatore = finanziatore;
        }

        public override string ToString()
        {
            return $"Id: {Id},Codice Edizione: {CodiceEdizione} Data di Inizio: {Start}, Data di Fine: {End}, Prezzo: {RealPrice}";
        }
    }
    // i delegati sono degli oggetti  ( è come creare una classe ) e questi delegati possono puntare a una o più funzioni
    // questo è un delegato per le funzioni senza parametri che restituiscono un intero
    //public delegate int AddStudents();
    //public delegate void EnrollStudents(int x);
}
