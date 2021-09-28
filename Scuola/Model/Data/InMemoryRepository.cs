using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using static Scuola.Model.Level;

namespace Scuola.Model.Data {
    public class InMemoryRepository : IRepository {
        private List<Corso> courses = new List<Corso>();
        private List<EdizioneCorso> courseEditions = new List<EdizioneCorso>();
        private ISet<Corso> courseSet = new HashSet<Corso>();
        private List<Azienda> aziende = new List<Azienda>();
        private static long lastIdCourse = 2;
        private static long lastIdEdition = 1;
        private static long lastIdProgetti = 2;
        private static long lastIdCategorie = 2;
        private static long lastIdAzienda = 3;
        private static long lastIdFinanziatore = 2;
        // è una struttura che rifiuta i duplicati, efficiente nel controllare la presenza o meno di elementi nel suo insieme 
        // quindi nel contains è efficace
        public InMemoryRepository(){
            Corso c = new Corso(
                id: 1,
                titolo: "CorsoX",
                ammontareOre: 240,
                livello: new Level(3203192, ExperienceLevel.MEDIO, "è un corso bello"),
                descrizione: "Questo è un corso bello e inventato",
                costoDiRiferimento: 2000,
                progetto: new Progetto(
                    id: 1,
                    descrizione: "questo è un bel progetto",
                    titolo: "Pierugolandia",
                    classAzienda: new Azienda(
                        id: 1,
                        nome: "GMG",
                        citta: "Treviso",
                        indirizzo: "Via delle Lavandaie",
                        cP: "00072",
                        telefono: "3922351915",
                        email: "gianmarco.bello97@gmail.com",
                        partitaIva: "29381923712"
                                            )
                    ),
                categoria: new Categoria(
                    id: 1,
                    categoriaCorso: Category.SISTEMISTICA,
                    descrizione: "mi sono rotto i cojoni"
                    )
               );
            courses.Add(c);
            EdizioneCorso ed = new EdizioneCorso(
                id: 1,
                start: new LocalDate(year: 1998, month: 11 , day: 11),
                end: new LocalDate(year: 1999, month: 11, day: 11),
                maxStudenti: 200,
                realPrice: 4000,
                inPresenza: true,
                aula: new Aula(
                    id: 1,
                    nome: "Aula Archimede",
                    capacitaMax: 200,
                    virtuale: true,
                    isComputerized: true,
                    hasProjector: true
                    ),
                corso: c,
                   finanziatore: new Finanziatore(
                        id: 1,
                        titolo: "Finanzia sta ceppa",
                        descrizione: "può bastare così"
                        )
                    );
            courseEditions.Add(ed);
            Azienda az = new Azienda(
                id: 3,
                nome: "GMG",
                citta: "Treviso",
                indirizzo: "Via delle Lavandaie",
                cP: "00072",
                telefono: "3922351915",
                email: "gianmarco.bello97@gmail.com",
                partitaIva: "29381923712"
                );
            
        }

        private Level Level(int v)
        {
            throw new NotImplementedException();
        }

        public Corso AddCourse2 (Corso c)
        {
            if(c.Id == 0)
            {
                c.Id = ++lastIdCourse;
            }
            bool added = courseSet.Add(c);
            return added ? c : null;
        }
        public Corso AddCourse(Corso newCoruse)
        {
            if (newCoruse.Id == 0)
            {
                newCoruse.Id = ++lastIdCourse;
            }
            if ( courses.Contains(newCoruse) ){
                Console.WriteLine("Questo corso è già esistente");
                return null;
            }
            else{
                courses.Add(newCoruse);
                return newCoruse;
            }
        }

        public Azienda AddAzienda(Azienda newAzienda)
        {
            if (newAzienda.Id == 0)
            {
                newAzienda.Id = ++lastIdAzienda;
            }
            if ( aziende.Contains(newAzienda) ){
                Console.WriteLine("Questa azienda è già esistente");
                return null;
            }
            else{
                aziende.Add(newAzienda);
                return newAzienda;
            }
        }

        public IEnumerable<EdizioneCorso> GetCourseEditions(long courseId)
        {
            List<EdizioneCorso> edizioniScelte = new List<EdizioneCorso>();
            foreach (var edizione in courseEditions){
                if (edizione.Corso.Id == courseId)
                {
                    edizioniScelte.Add(edizione);
                }
            }
            return edizioniScelte;
        }
        public IEnumerable<Corso> GetCourses()
        {
            return courses;
        }
        // anche se si aspetta un Enumerable, dato che la lista è una sottoclasse di Enumerable allora può essere fatto
        public EdizioneCorso AddEdition(EdizioneCorso ed){
            if(ed.Id == 0)
            {
                ed.Id = ++lastIdEdition;
            }
            courseEditions.Add(ed);
            return ed;
        }
        public Corso FindById( long id ){
            foreach (var c in courses){
                if (c.Id == id)
                    return c;
            }
            return null;
        }
        public Corso BETTERFindById ( long id){
            Corso found = courses.SingleOrDefault((Corso c) => {
                return c.Id == id;
            });
            return found;
            // Se ho più di un input per la espressione lambda allora metto (a,b,c)
        }

        public IEnumerable<EdizioneCorso> FindEditionByCourses(long courseId){
            List<EdizioneCorso> editions = new List<EdizioneCorso>();
            foreach (var ed in courseEditions){
                if (ed.Corso.Id == courseId){
                    editions.Add(ed);
                }
            }
            return editions;
        }

        public Azienda FindAziendaById(long id)
        {
            Azienda found = aziende.SingleOrDefault((Azienda a) =>
            {
                return a.Id == id;
            });
            return found;
        }
    }
}
