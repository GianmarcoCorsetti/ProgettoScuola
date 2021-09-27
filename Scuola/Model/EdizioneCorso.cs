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
        private AddStudents ad;
        public EdizioneCorso(long id, Corso nomeCorso, LocalDate start, LocalDate end, int numStudents, decimal realPrice){
            Id = id;
            NomeCorso = nomeCorso;
            Start = start;
            End = end;
            NumStudents = numStudents;
            RealPrice = realPrice;
            ad += Iscrivi;
        }
        public void AggiornaEdizione()
        {
            NumStudents += ad();
        }
        public void ChangeAdder( AddStudents x)
        {
            ad = x;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Titolo del corso: {NomeCorso.Titolo}, Data di Inizio: {Start}, Data di Fine: {End}, Prezzo: {RealPrice}";
        }
        public int Iscrivi(){
            return 10;
        }
    }
    // i delegati sono degli oggetti  ( è come creare una classe ) e questi delegati possono puntare a una o più funzioni
    // questo è un delegato per le funzioni senza parametri che restituiscono un intero
    public delegate int AddStudents();
    public delegate void EnrollStudents(int x);
}
