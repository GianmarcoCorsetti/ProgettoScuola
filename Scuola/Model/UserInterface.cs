using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Text;
using Scuola.Model.Data;
using static System.Console;

namespace Scuola.Model {
    public class UserInterface {
        private bool isDatabaseSource = false;
        public CourseService CourseService { get; set; }
        const string DIVISORE = "********************************************************";
        const string MAIN_MENU = "Operazioni disponibili: \n " +
            " - inserisci 'a' per vedere tutti i corsi \n " +
            " - inserisci 'b' per per vedere tutte le aziende \n "+
            " - inserisci 'c' per vedere tutti i progetti \n " +
            " - inserisci 'd' per cercare le edizioni di un corso \n " +
            " - inserisci 'e' per inserire una nuova edizione di un corso \n " +
            " - inserisci 'f' per inserire un nuovo corso \n " +
            " - Inserisci 'g' per inserire una nuova azienda \n " +
            " - inserisci 'h' per inserire un nuovo progetto \n " +
            " - inserisci 'i' per inserire una nuova cataegoria \n " +
            " - inserisci 'w' per inserire una nuova aula \n " +
            " - inserisci 'y' per inserire un nuovo finanziatore \n " +
            " - inserisci 'z' per inserire un nuovo livello \n " +
            " - inserisci 'x' per passare da locale a Database \n "+
            " - inserisci 'v' per passare dal Database a locale \n " +
            " - inserisci 'q' per uscire dal menù \n ";
        const string BASE_PROMPT = " => ";
        public UserInterface (CourseService service){
            CourseService = service;
        }
        // Nuovo caso d'uso l'utente inserisce un id di un corso, e il programma risponde mostrando il numero di edizioni che esistono del corso
        // la somma dei prezzi delle edizioni, il valor medio del prezzo delle edizioni, la mediana del prezzo delle edizioni,
        // la moda dei prezi delle edizioni e il numero massimo e minimo di studenti nelle edizioni. 
        public void Start() {
            bool quit = false;
            do {
                WriteLine(DIVISORE);
                WriteLine(MAIN_MENU);
                char c = ReadChar(BASE_PROMPT);
                switch (c)
                {
                    case 'a':
                        ShowCourses();
                        break;
                    case 'b':
                        ShowCompanies();
                        break;
                    case 'c':
                        ShowProjects();
                        break;
                    case 'd':
                        ShowCourseEditionsByCourse();
                        break;
                    case 'e':
                        CreateCourseEdition();
                        break;
                    case 'f':
                        CreateCourse();
                        break;
                    case 'g':
                        CreateAzienda();
                        break;
                    case 'h':
                        CreateProgetto();
                        break;
                    case 'i':
                        CreateCategoria();
                        break;
                    case 'w':
                        CreateAula();
                        break;
                    case 'y':
                        CreateFinanziatore();
                        break;
                    case 'z':
                        CreateLivello();
                        break;
                    case 'x':
                        CambiaADatabase();
                        break;
                    case 'v':
                        CambiaALocale();
                        break;
                    case 'q':
                        quit = true;
                        break;
                    default:
                        WriteLine("Comando non riconosciuto");
                        break;
                }
            } while (!quit);
        }

        private void CambiaALocale()
        {
            if( !isDatabaseSource)
            {
                Console.WriteLine("Sei già in datasource locale");
                return;
            }
            CourseService.Repository = new InMemoryRepository();
            isDatabaseSource = false;
            Console.WriteLine("Sorgente dati cambiata con successo a Locale");
        }
        private void CambiaADatabase()
        {
            if (isDatabaseSource)
            {
                Console.WriteLine("Sei già in datasource Database");
                return;
            }
            CourseService.Repository = new DatabaseRepository();
            isDatabaseSource = true;
            Console.WriteLine("Sorgente dati cambiata con successo a Database");
        } 

        public void ShowStatisticalReport()
        {
            long idCorso = ReadLong("Inserire l'id del corso : ");
            Report rp = CourseService.GenerateStatisticalReport(idCorso);
            rp.ToString();
        }

        public void ShowCompanies()
        {
            IEnumerable<Azienda> companies = CourseService.GetAllAzienda();
        }

        public void ShowProjects()
        {
            IEnumerable<Progetto> Projects = CourseService.GetAllProjects();
        }

        public void CreateCourseEdition(){
            LocalDate start = ReadLocalDate("Inserire la data di inizio del corso (yyyy-mm-dd) : ");
            LocalDate finish = ReadLocalDate("Inserire la data di fine del corso (yyyy-mm-dd) : ");
            int maxStudents = (int)ReadLong("Inserire il numero di studenti massimo che potranno seguire il corso: ");
            decimal realPrice = ReadDecimal("Inserire il prezzo finale dell'edizione del corso : ");
            bool inPresenza = ReadBool("Inserire il valore booleano riguardante lo stato di presenza o meno dell corso: ");
            long id_aula = ReadLong("Inserisci l'id dell'aula: ");
            long id_corso = ReadLong("Inserisci l'id del corso: ");
            long id_finanziatore = ReadLong("Inserisci l'id del finanziatore: ");
            var edition = new EdizioneCorso(
                    id: 0,
                    start: start,
                    end: finish,
                    maxStudenti: maxStudents,
                    realPrice: realPrice,
                    inPresenza: inPresenza,
                    aula: CourseService.GetAula(id_aula),
                    corso: CourseService.GetCourse(id_corso), 
                    finanziatore: CourseService.GetFinanziatore(id_finanziatore)
                );
        }

        private void CreateAula()
        {
            // nome, capacità max, virtuale, isComputerized, HasProjector
            string nome = ReadString("Inserire il nome dell'aula: ");
            int capacitaMax = (int)ReadLong("Inserire il numero massimo di stuenti che possono essere presenti nell'aula: ");
            bool isVirtual = ReadBool("Inserire il valore booleano riguardante lo stato di presenza fisica o meno dell'aula: ");
            bool isComputerized = ReadBool("Inserire il valore booleano riguardante la presenza o meno dei computer nell'aula: ");
            bool hasProjector = ReadBool("Inserire il valore booleano riguardante la presenza o meno del proiettore nell'aula: ");
            Aula aula = new Aula(
                    id: 0,
                    nome: nome,
                    capacitaMax: capacitaMax,
                    virtuale: isVirtual,
                    isComputerized: isComputerized,
                    hasProjector: hasProjector
                );

            CourseService.CreateAula(aula);
            Console.WriteLine("Aula inserita con successo");
            
        }

        private void CreateFinanziatore()
        {
            //id, titolo, descrizione
            string titolo = ReadString("Inserire il titolo del finanziatore: ");
            string descrizione = ReadString("Inserire la descrizione del finanziotore: ");
            Finanziatore finanziatore = new Finanziatore(id: 0, titolo: titolo, descrizione: descrizione);
            CourseService.CreateFinanziatore(finanziatore);
            Console.WriteLine("Finanziatore inserito con successo");
        }

        private void ShowCourses(){
            IEnumerable<Corso> courses = CourseService.GetAllCourses();
            foreach (var c in courses){
                WriteLine(c.ToString());
            }
        }

        private bool ReadBool(string prompt){
            string boolString = ReadString(prompt);
            bool boolIsGood = false;
            bool boolVal;
            do
            {
                boolString = ReadString("Inserire 'True' altrimenti 'False' : ");
                boolIsGood = bool.TryParse(boolString, out boolVal);
                if (!boolIsGood)
                {
                    WriteLine("Inserisci un valore valido! ");
                }
            }
            while (!boolIsGood);
            return boolVal;
        }

        private void CreateLivello()
        {
            string descrizione = ReadString("Inserisci la descrizione del livello: ");
            ExperienceLevel experienceLevel = ReadExperienceLevel("Inserire il livello del corso tra le seguenti : " +
                "PRINCIPIANTE - MEDIO - ESPERTO - GURU: ");
            
            Livello livello = new Livello(id: 0, descrizione: descrizione, livelloCorso: experienceLevel);
            CourseService.CreateLivello(livello);
            Console.WriteLine("Livello inserito con successo");
        }

        private ExperienceLevel ReadExperienceLevel(string prompt)
        {
            bool ExpIsGood = false;
            ExperienceLevel level;
            do
            {
                string livelloString = ReadString(prompt);
                ExpIsGood = Enum.TryParse(livelloString, out level);
                if (!ExpIsGood)
                {
                    WriteLine("Inserisci un livello valido");
                }
                
            }
            while (!ExpIsGood);
            return level;
        }

        private void CreateCategoria()
        {
            string descrizione = ReadString("Inserisci la descrizione della categoria: ");
            Category category = ReadCategoria("Inserire la categoria tra le seguenti: " +
                "GRAFICA, SVILUPPOSOFTWARE, LINGUE, SISTEMISTICA");
            Categoria categoria = new Categoria(0, categoriaCorso: category, descrizione);
            CourseService.CreateCategoria(categoria);
            Console.WriteLine("Categoria inserita con successo");
        }

        private Category ReadCategoria (string prompt)
        {
            bool ExpIsGood = false;
            Category cat;
            do
            {
                string catString = ReadString(prompt);
                ExpIsGood = Enum.TryParse(catString, out cat);
                if (!ExpIsGood)
                {
                    WriteLine("Inserisci un livello valido");
                }
            }
            while (!ExpIsGood);
            return cat;
        }

        private void CreateProgetto()
        {
            string titolo = ReadString("Inserisci il titolo del progetto: ");
            string descrizione = ReadString("Inserisci la descrizione del progetto: ");
            long id_azienda = ReadLong("Inserisci l'id dell'azienda: ");
            Progetto progetto = new Progetto(0, titolo, descrizione, CourseService.GetAzienda(id_azienda));
            CourseService.CreateProgetto(progetto);
            Console.WriteLine("Progetto inserito con successo");
        }

        private void CreateAzienda()
        {
            string nome = ReadString("Inserire il nome dell'azienda: ");
            string citta = ReadString("Inserire il nome della città in cui ha sede l'azienda: ");
            string indirizzo = ReadString("Inserire l'indirizzo dell'azienda: ");
            string cP = ReadString("inserire il codice postale dell'azienda: ");
            string telefono = ReadString("Inserire il numerod di telefono: ");
            string email = ReadString("inserisci l'email dell'azienda: ");
            string partitaIva = ReadString("inserisci la partita iva dell'azienda: ");
            Azienda azienda = new Azienda(
                    id: 0,
                    nome: nome,
                    citta: citta, 
                    indirizzo: indirizzo, 
                    cP:cP,
                    telefono: telefono, 
                    email: email,
                    partitaIva: partitaIva
                );
            CourseService.CreateAzienda(azienda);
            Console.WriteLine("Azienda inserita con successo");
        }
        
        private void CreateCourse(){ 
            string titolo = ReadString("Inserisci il titolo del corso: "); 
            int ammontareOre = (int)ReadLong("Inserisci il numero di ore del corso: ");
            string descrizione = ReadString("Inserisci una descrizione del corso: ");
            decimal costoRiferimento = ReadDecimal("Inserisci il costo di riferimento del corso: ");
            long id_livello = ReadLong("Inserisci l'id del livello: ");
            long id_progetto = ReadLong("Inserisci l'id del progetto: "); 
            long id_categoria = ReadLong("Inserisci l'id della categoria: ");
            Corso c = new Corso(
                id: 0,
                titolo: titolo,
                ammontareOre: ammontareOre,
                descrizione: descrizione,
                costoDiRiferimento: costoRiferimento,
                idLivello: id_livello,      //CourseService.GetLivello(id_livello),
                idProgetto: id_progetto,     //CourseService.GetProgetto(id_progetto),
                idCategoria: id_categoria     //CourseService.GetCategoria(id_categoria)
            ); ;
            CourseService.CreateCourse(c);
            WriteLine("Corso inserito con successo");
        }
        
        private void ShowCourseEditionsByCourse(){ // Da controllare, penso ok
            long id = ReadLong("Inserisci l'id del corso: ");
            IEnumerable<EdizioneCorso> editions = CourseService.GetCourseEdition(id);
            if( editions == null){
                WriteLine("Errore: Non esistono edizioni di questo corso");
            }
            foreach (var c in editions){
                if(c.Id == id)
                    WriteLine(c.ToString());
            }
        }

        private LocalDate ReadLocalDate(string prompt)
        {
            bool success = false;
            LocalDate date;
            do
            {
                string answer = ReadString(prompt);
                success = TryParse(answer, out date);
            } while (!success);
            return date;
        }

        private bool TryParse(string input, out LocalDate date)
        {

            var pattern = LocalDatePattern.CreateWithInvariantCulture("yyyy-mm-dd");
            var parseResult = pattern.Parse(input);
            date = parseResult.GetValueOrThrow();
            return true;
        }

        private decimal ReadDecimal(string prompt){
            bool isNumber = false;
            decimal numdec;
            do
            {
                string answer = ReadString(prompt);
                isNumber = decimal.TryParse(answer, out numdec);
                if (!isNumber)
                {
                    WriteLine("Riprova ad inserire un numero");
                }
            } while (!isNumber);
            return numdec;
        }

        private string ReadString(string prompt)
        {
            string answer = null;
            do
            {
                Write(prompt);
                answer = ReadLine().Trim();
                if (string.IsNullOrEmpty(answer))
                {
                    WriteLine("Hai inserito un messaggio vuoto riprova l'inserimento");
                }
            } while (string.IsNullOrEmpty(answer));
            return answer;
        }

        private char ReadChar(string prompt){
            return ReadString(prompt)[0];
        }

        private long ReadLong (string prompt){
            bool isNumber = false;
            long num;
            do
            {
                string answer = ReadString(prompt);
                isNumber = long.TryParse(answer, out num);
                if (!isNumber){
                    WriteLine("Riprova ad inserire un numero");
                }
            } while (!isNumber);
            return num;
        }
    }
}
