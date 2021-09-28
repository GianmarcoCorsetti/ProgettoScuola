using System;
using System.Linq;
using Scuola.Model;
using Scuola.Model.Data;
using Scuola.Utilites;

namespace Scuola {
    class Program {
        static void Main(string[] args)
        {
            // IRepository repo = new InMemoryRepository();
            IRepository repo = new DatabaseRepository();
            CourseService cs = new CourseService(repo);
            UserInterface ui = new UserInterface(cs);
            //// iniezione delle dipendenze : il soggetto a cui viene iniettata la indipendenza può non sapere che indipendenza è
            ui.Start();

            // 1
            // Modificare le classi edizioneCorso e Corso in base alle specifiche presenti nel Database, così da essere
            // coerenti rispetto al db. E modificare il codice esistente di conseguenza. 

            // 2
            // Creare una seconda implementazione di IRepository che va sul DB, che tramite le chiamate ADO.Net riesce 
            // a gestire le modifiche e le richieste sul DB 

            //EdizioneCorso ed = new EdizioneCorso(3, null, new NodaTime.LocalDate(2011, 6, 20), new NodaTime.LocalDate(2021, 2, 4), 21, 40);
            //Console.WriteLine(ed.NumStudents);
            //ed.AggiornaEdizione();
            //Console.WriteLine(ed.NumStudents);
            //ed.ChangeAdder(new AddStudents(Enroll)); // o (Enroll) o direttamente (()=> 20)
            //ed.AggiornaEdizione();
            //Console.WriteLine(ed.NumStudents);

        }
    }
}
