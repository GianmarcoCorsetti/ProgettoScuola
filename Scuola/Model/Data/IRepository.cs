using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model.Data {
    public interface IRepository {
        IEnumerable<Corso> getCourses();
        Corso AddCourse(Corso newCoruse);
        IEnumerable<EdizioneCorso> getCourseEditions(long courseId);

    }
}
