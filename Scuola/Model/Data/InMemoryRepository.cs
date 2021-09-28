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
        // è una struttura che rifiuta i duplicati, efficiente nel controllare la presenza o meno di elementi nel suo insieme 
        // quindi nel contains è efficace
        public InMemoryRepository(){
            Corso c = new Corso(
                id: 241499,
                titolo: "CorsoX",
                ammontareOre: 240,
                livello: new Level(3203192, ExperienceLevel.MEDIO, "è un corso bello"),
                descrizione: "Questo è un corso bello e inventato",
                costoDiRiferimento: 2000,
                project: new Progetti(
                    id: 103913,
                    descrizione: "questo è un bel progetto",
                    titolo: "Pierugolandia",
                    classAzienda: new Azienda(
                        id: 328832,
                        nome: "GMG",
                        citta: "Treviso",
                        indirizzo: "Via delle Lavandaie",
                        cP: "00072",
                        telefono: "3922351915",
                        email: "gianmarco.bello97@gmail.com",
                        partitaIva: "29381923712"
                                            )
                    ),
                category: new Categoria(
                    id: 361273,
                    categoriaCorso: Category.SISTEMISTICA,
                    descrizione: "mi sono rotto i cojoni"
                    )
               );
            courses.Add(c);
            EdizioneCorso ed = new EdizioneCorso(
                id: 241499,
                start: new LocalDate(year: 1998, month: 11 , day: 11),
                end: new LocalDate(year: 1999, month: 11, day: 11),
                maxStudenti: 200,
                realPrice: 4000,
                inPresenza: true,
                aule: new Aula(
                    id: 31623,
                    nome: "Aula Archimede",
                    capacitaMax: 200,
                    virtuale: true,
                    isComputerized: true,
                    hasProjector: true
                    ),
                classCorso: new Corso(
                    id: 241499,
                    titolo: "CorsoX",
                    ammontareOre: 240,
                    livello: new Level(3203192, ExperienceLevel.MEDIO, "è un corso bello"),
                    descrizione: "Questo è un corso bello e inventato",
                    costoDiRiferimento: 2000,
                    project: new Progetti(
                        id: 103913,
                        descrizione: "questo è un bel progetto",
                        titolo: "Pierugolandia",
                        classAzienda: new Azienda(
                            id: 328832,
                            nome: "GMG",
                            citta: "Treviso",
                            indirizzo: "Via delle Lavandaie",
                            cP: "00072",
                            telefono: "3922351915",
                            email: "gianmarco.bello97@gmail.com",
                            partitaIva: "29381923712"
                                                )
                        ),
                    category: new Categoria(
                        id: 361273,
                        categoriaCorso: Category.SISTEMISTICA,
                        descrizione: "mi sono rotto i cojoni"
                        )
                   ),
                   classFinanziatore: new Finanziatore(
                        id: 6371823,
                        titolo: "Finanzia sta ceppa",
                        descrizione: "può bastare così"
                        )
                    );
            courseEditions.Add(ed);
        }

        private Level Level(int v)
        {
            throw new NotImplementedException();
        }

        public Corso AddCourse2 (Corso c)
        {
            bool added = courseSet.Add(c);
            return added ? c : null;
        }
        public Corso AddCourse(Corso newCoruse)
        {
            if( courses.Contains(newCoruse) ){
                Console.WriteLine("Questo corso è già esistente");
                return null;
            }
            else{
                courses.Add(newCoruse);
                return newCoruse;
            }
        }
        public IEnumerable<EdizioneCorso> getCourseEditions(long courseId)
        {
            List<EdizioneCorso> edizioniScelte = new List<EdizioneCorso>();
            foreach (var edizione in courseEditions){
                if (edizione.ClassCorso.Id == courseId)
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
                if (ed.ClassCorso.Id == courseId){
                    editions.Add(ed);
                }
            }
            return editions;
        }
    }
}
