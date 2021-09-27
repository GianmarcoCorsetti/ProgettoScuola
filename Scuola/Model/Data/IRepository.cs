using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model.Data {
    public interface IRepository {
        IEnumerable<Corso> GetCourses();
        Corso AddCourse(Corso newCoruse);
        IEnumerable<EdizioneCorso> getCourseEditions(long courseId);
        EdizioneCorso AddEdition(EdizioneCorso ed);
        Corso FindById(long id);
        IEnumerable<EdizioneCorso> FindEditionByCourses(long courseId);

    }
}
