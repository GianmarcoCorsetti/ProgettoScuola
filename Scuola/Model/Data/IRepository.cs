using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model.Data {
    public interface IRepository {
        IEnumerable<Corso> GetCourses();
        Corso AddCourse(Corso newCoruse);
        IEnumerable<EdizioneCorso> GetCourseEditions(long courseId);
        EdizioneCorso AddEdition(EdizioneCorso ed);
        Corso FindById(long id);
        Azienda FindAziendaById(long id);
        Azienda AddAzienda(Azienda newAzienda);
    }
}
