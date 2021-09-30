using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model.Data {
    public interface IRepository {
        IEnumerable<Corso> GetCourses();
        IEnumerable<Azienda> GetAziendas();
        IEnumerable<Progetto> GetProgettos();
        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Aula> GetAulas();
        IEnumerable<Finanziatore> GetFinanziatores();
        IEnumerable<EdizioneCorso> GetCourseEditions(long courseId);
        Azienda AddAzienda(Azienda newAzienda);
        Corso AddCourse(Corso newCoruse);
        Aula AddAula(Aula newAula);
        Finanziatore AddFinanziatore(Finanziatore newFinanziatore);
        Livello AddLivello(Livello newLivello);
        Categoria AddCategoria(Categoria newCategoria);
        Progetto AddProgetto(Progetto newProgetto);
        EdizioneCorso AddEdition(EdizioneCorso ed);
        Corso FindCourseById(long id);
        Aula FindAulaById(long id);
        Finanziatore FindFinanziatoreById(long id);
        Azienda FindAziendaById(long id);
        Livello FindLivelloById(long id);
        Categoria FindCategoriaById(long id);
        Progetto FindProgettoById(long id);

    }
}
