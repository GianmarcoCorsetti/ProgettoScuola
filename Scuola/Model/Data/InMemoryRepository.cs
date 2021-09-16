using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model.Data {
    class InMemoryRepository : IRepository {
        private List<Corso> courses = new List<Corso>();
        private List<EdizioneCorso> courseEditions = new List<EdizioneCorso>();
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
        public IEnumerable<Corso> getCourses() 
        {
            return courses;
        }
        // anche se si aspetta un Enumerable, dato che la lista è una sottoclasse di Enumerable allora può essere fatto
    }
}
