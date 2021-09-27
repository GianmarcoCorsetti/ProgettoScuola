using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace Scuola.Model.Data {
    public class InMemoryRepository : IRepository {
        private List<Corso> courses = new List<Corso>();
        private List<EdizioneCorso> courseEditions = new List<EdizioneCorso>();
        private ISet<Corso> courseSet = new HashSet<Corso>();
        // è una struttura che rifiuta i duplicati, efficiente nel controllare la presenza o meno di elementi nel suo insieme 
        // quindi nel contains è efficace
        public InMemoryRepository(){
            Corso c = new Corso(
                newId: 241499, 
                newTitolo: "CorsoX", 
                newDurataInOre: 240, 
                newLevel: ExperienceLevel.MEDIO,
                newDescription: "Questo è un corso bello e inventato" ,
                newStandardPrice: 2000);
            courses.Add(c);
            EdizioneCorso ed = new EdizioneCorso(
                id: 241499,
                nomeCorso: c,
                start: new LocalDate(year: 1998, month: 11 , day: 11),
                end: new LocalDate(year: 1999, month: 11, day: 11),
                numStudents: 200,
                realPrice: 4000
                );
            courseEditions.Add(ed);
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
                if (edizione.NomeCorso.Id == courseId)
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
                if (ed.NomeCorso.Id == courseId){
                    editions.Add(ed);
                }
            }
            return editions;
        }
    }
}
