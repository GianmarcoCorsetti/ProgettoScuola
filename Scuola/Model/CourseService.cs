using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scuola.Model.Data;

namespace Scuola.Model {
    public class CourseService {
        public IRepository Repository { get; set; }
        // passo l'interfaccia perché potrò passare poi un qualsiasi oggeto
        // che implementa l'interfaccia, in modo tale da poter essere più generico possibile
        public CourseService ( IRepository repo){
            Repository = repo;
        }

        public Azienda CreateAzienda(Azienda a)
        {
            return Repository.AddAzienda(a);
        }

        public Livello CreateLivello(Livello l)
        {
            return Repository.AddLivello(l);
        }

        public Aula CreateAula(Aula c)
        {
            return Repository.AddAula(c);
        }

        public Finanziatore CreateFinanziatore(Finanziatore f)
        {
            return Repository.AddFinanziatore(f);
        }
        
        public Categoria CreateCategoria(Categoria c)
        {
            return Repository.AddCategoria(c);
        }

        public Progetto CreateProgetto(Progetto p)
        {
            return Repository.AddProgetto(p);
        }

        public Corso CreateCourse(Corso c)
        {
            return Repository.AddCourse(c);
        }

        public IEnumerable<Corso> GetAllCourses()
        {
            return Repository.GetCourses();
        }

        public IEnumerable<Azienda> GetAllAzienda()
        {
            return Repository.GetAziendas();
        }

        public IEnumerable<Progetto> GetAllProjects()
        {
            return Repository.GetProgettos();
        }

        public IEnumerable<Categoria> GetAllCategories()
        {
            return Repository.GetCategorias();
        }

        public Corso GetCourse(long id){
            return Repository.FindCourseById(id);
        }

        public Aula GetAula(long id){
            return Repository.FindAulaById(id);
        }

        public Finanziatore GetFinanziatore(long id){
            return Repository.FindFinanziatoreById(id);
        }

        public Azienda GetAzienda(long id){
            return Repository.FindAziendaById(id);
        }

        public Livello GetLivello(long id){
            return Repository.FindLivelloById(id);
        }

        public Categoria GetCategoria(long id)
        {
            return Repository.FindCategoriaById(id);
        }

        public Progetto GetProgetto(long id){
            return Repository.FindProgettoById(id);
        }

        public IEnumerable<EdizioneCorso> GetCourseEdition(long id){
            return Repository.GetCourseEditions(id);
        }

        public EdizioneCorso CreateCourseEdition(EdizioneCorso ed, long idCourse){
            Corso found = Repository.FindCourseById(idCourse);
            if(found == null){
                return null;
            }
            ed.Corso = found;
            Repository.AddEdition(ed);
            return ed;
        }

        public Report GenerateStatisticalReport(long id){
            // vediamo intanto se il corso esiste: 
            Report report = new Report();
            Corso found = Repository.FindCourseById(id);
            if (found == null)
            {
                return null;
            }
            IEnumerable<EdizioneCorso> editions = Repository.GetCourseEditions(id);
            report.NumEdition = editions.Count();
            report.SumPrices = editions.Sum(e => e.RealPrice);
            report.AveragePrice = report.SumPrices / report.NumEdition;
            report.MedPrice = CalculateMedianPrice(editions);
            report.ModaPrice = CalculateModa(editions);
            //report.MaxStudents = editions.Max(e => e.NumStudents);
            //report.MaxStudents = editions.Min(e => e.NumStudents);
            return report;
        }

        private decimal CalculateMedianPrice (IEnumerable<EdizioneCorso> editions){
            var prices = editions.Select(a => a.RealPrice).OrderBy(r => r).ToList();
            if (prices.Count % 2 != 0)
            {
                return prices[(prices.Count / 2)];
            }
            return (prices[prices.Count / 2] + prices[prices.Count / 2 - 1]) / 2;
        }

        private decimal CalculateModa(IEnumerable<EdizioneCorso> editions){
            List<decimal> EditionsInDictionary = new List<decimal>();
            foreach (var element in editions)
            {
                EditionsInDictionary.Add(element.RealPrice);
            }
            decimal[] modeArray = EditionsInDictionary.ToArray();
            var mode = modeArray.GroupBy(x => x).OrderByDescending(a => a.Count()).Select(b => b.Key).FirstOrDefault();
            return mode;
        }
    }
}
