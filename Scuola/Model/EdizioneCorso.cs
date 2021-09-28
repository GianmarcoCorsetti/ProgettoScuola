using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class EdizioneCorso{
        public long Id { get; set; }
        public LocalDate Start { get; set; }
        public LocalDate End{ get; set; }
        public int MaxStudenti { get; set; }
        public decimal RealPrice { get; set; }
        public bool InPresenza{ get; set; }
        public Aula Aule { get; set; }
        public Corso ClassCorso { get; set; }
        public Finanziatore ClassFinanziatore { get; set; }
        public EdizioneCorso(long id, LocalDate start, LocalDate end, int maxStudenti, decimal realPrice, bool inPresenza, Aula aule, Corso classCorso, Finanziatore classFinanziatore)
        {
            Id = id;
            Start = start;
            End = end;
            MaxStudenti = maxStudenti;
            RealPrice = realPrice;
            InPresenza = inPresenza;
            Aule = aule;
            ClassCorso = classCorso;
            ClassFinanziatore = classFinanziatore;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Titolo del corso: {ClassCorso.Titolo}, Data di Inizio: {Start}, Data di Fine: {End}, Prezzo: {RealPrice}";
        }
    }
    // i delegati sono degli oggetti  ( è come creare una classe ) e questi delegati possono puntare a una o più funzioni
    // questo è un delegato per le funzioni senza parametri che restituiscono un intero
    //public delegate int AddStudents();
    //public delegate void EnrollStudents(int x);
}
