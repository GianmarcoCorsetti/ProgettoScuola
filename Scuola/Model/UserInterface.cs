using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Text;
using static System.Console;

namespace Scuola.Model {
    public class UserInterface {
        public CourseService CourseService { get; set; }
        const string DIVISORE = "********************************************************";
        const string MAIN_MENU = "Operazioni disponibili: \n " +
            " - inserisci 'a' per vedere tutti i corsi \n " +
            " - inserisci 'b' per inserire un corso \n " +
            " - inserisci 'c' per inserire una nuova edizione di un corso \n " +
            " - inserisci 'd' per cercare le edizioni di un corso \n " +
            " - Inserisci 'e' per poter fare un report statistico del corso \n " +
            " - Inserisci 'f' per creare un'azienda \n " +
            " - inserisci 'q' per uscire dal menù \n";
        const string BASE_PROMPT = "=> ";
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
                        CreateACourse();
                        break;
                    case 'c':
                        CreateCourseEdition();
                        break;
                    case 'd':
                        ShowCourseEditionsByCourse();
                        break;
                    case 'e':
                        ShowStatisticalReport();
                        break;
                    case 'f':
                        CreateAzienda();
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
        public void ShowStatisticalReport() // OK
        {
            long idCorso = ReadLong("Inserire l'id del corso :");
            Report rp = CourseService.GenerateStatisticalReport(idCorso);
            rp.ToString();
        }
        public void CreateCourseEdition(){ //OK
            LocalDate start = ReadLocalDate("Inserire la data di inizio del corso (yyyy-mm-dd) : ");
            LocalDate finish = ReadLocalDate("Inserire la data di fine del corso (yyyy-mm-dd) : ");
            int maxStudents = (int)ReadLong("Inserire il numero di studenti massimo che potranno seguire il corso: ");
            decimal realPrice = ReadDecimal("Inserire il prezzo finale dell'edizione del corso : ");
            bool inPresenza = ReadBool("Inserire il valore booleano riguardante lo stato di presenza o meno dell corso: ");
            Aula aula = CreateAula();
            Corso corso = CreateCourse();
            Finanziatore finanziatore = CreateFinanziatore();
            var edition = new EdizioneCorso(
                    id: 0,
                    start: start,
                    end: finish,
                    maxStudenti: maxStudents,
                    realPrice: realPrice,
                    inPresenza: inPresenza,
                    aula: aula,
                    corso: corso, 
                    finanziatore: finanziatore
                );
        }
        private Aula CreateAula()
        {
            // nome, capacità max, virtuale, isComputerized, HasProjector
            string nome = ReadString("Inserire il nome dell'aula: ");
            int capacitaMax = (int)ReadLong("Inserire il numero massimo di stuenti che possono essere presenti nell'aula");
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
            return aula;
        }
        private Finanziatore CreateFinanziatore()
        {
            //id, titolo, descrizione
            string titolo = ReadString("Inserire il titolo del finanziatore: ");
            string descrizione = ReadString("Inserire la descrizione del finanziotore: ");
            Finanziatore finanziatore = new Finanziatore(id: 0, titolo: titolo, descrizione: descrizione);
            return finanziatore;
        }
        private void ShowCourses(){ // OK 
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

        private Level CreateLevel()
        {
            //long id = ReadLong("Inserisci l'id del livello: ");
            string descrizione = ReadString("Inserisci la descrizione del livello: ");
            ExperienceLevel experienceLevel = ReadExperienceLevel("Inserire il livello del corso tra le seguenti : " +
                "PRINCIPIANTE - MEDIO - ESPERTO - GURU: ");
            
            return new Level(id: 0, descrizione: descrizione, livelloCorso: experienceLevel);
        }

        private ExperienceLevel ReadExperienceLevel(string prompt)
        {
            string livelloString = ReadString(prompt);
            bool ExpIsGood = false;
            ExperienceLevel level;
            do
            {
                livelloString = ReadString("Inserire Livello del corso: PRINCIPIANTE | MEDIO | ESPERTO | GURU =>");
                ExpIsGood = Enum.TryParse(livelloString, out level);
                if (!ExpIsGood)
                {
                    WriteLine("Inserisci un livello valido");
                }
            }
            while (!ExpIsGood);
            return level;
        }

        private Categoria CreateCategoria()
        {
            // id , categoria, descrizione
            //long id = ReadLong("Inserisci l'id della categoria: ");
            string descrizione = ReadString("Inserisci la descrizione della categoria: ");
            Category categoria = ReadCategoria("Inserire la categoria tra le seguenti : " +
                "GRAFICA, SVILUPPOSOFTWARE, LINGUE, SISTEMISTICA");
            return new Categoria(0, categoriaCorso: categoria, descrizione);
        }

        private Category ReadCategoria (string prompt)
        {
            string catString = ReadString(prompt);
            bool ExpIsGood = false;
            Category cat;
            do
            {
                catString = ReadString("Inserire Livello del corso: PRINCIPIANTE | MEDIO | ESPERTO | GURU =>");
                ExpIsGood = Enum.TryParse(catString, out cat);
                if (!ExpIsGood)
                {
                    WriteLine("Inserisci un livello valido");
                }
            }
            while (!ExpIsGood);
            return cat;
        }

        private Progetto CreateProgetto()
        {
            //long id = ReadLong("Inserisci l'id del progetto: ");
            string titolo = ReadString("Inserisci il titolo del progetto: ");
            string descrizione = ReadString("Inserisci la descrizione del progetto: ");
            long id_azienda = ReadLong("Inserisci l'id dell'azienda");

            return new Progetto(0, titolo, descrizione, CourseService.GetAzienda(id_azienda));
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
        
        private void CreateACourse(){ // Da fare
            //long id = ReadLong("Inserisci l'id del corso: "); // ok
            string titolo = ReadString("Inserisci il titolo del corso: "); // ok 
            int ammontareOre = (int)ReadLong("Inserisci il numero di ore del corso: ");// ok
            string descrizione = ReadString("Inserisci una descrizione del corso: ");// ok
            decimal costoRiferimento = ReadDecimal("Inserisci il costo di riferimento del corso: ");// ok
            Level livello = CreateLevel(); // Creo il livello
            Progetto progetto = CreateProgetto(); // Creo il progetto
            Categoria categoria = CreateCategoria(); // Creo la categoria 
            Corso c = new Corso(
                id: 0,
                titolo: titolo,
                ammontareOre: ammontareOre,
                descrizione: descrizione,
                costoDiRiferimento: costoRiferimento,
                livello: livello,
                progetto: progetto,
                categoria: categoria
            );
            CourseService.CreateCourse(c);
            WriteLine("Corso inserito con successo");
        }
        private Corso CreateCourse(){ 
            //long id = ReadLong("Inserisci l'id del corso: "); // ok
            string titolo = ReadString("Inserisci il titolo del corso: "); // ok 
            int ammontareOre = (int)ReadLong("Inserisci il numero di ore del corso: ");// ok
            string descrizione = ReadString("Inserisci una descrizione del corso: ");// ok
            decimal costoRiferimento = ReadDecimal("Inserisci il costo di riferimento del corso: ");// ok
            Level livello = CreateLevel(); // Creo il livello
            Progetto progetto = CreateProgetto(); // Creo il progetto
            Categoria categoria = CreateCategoria(); // Creo la categoria 
            Corso c = new Corso(
                id: 0,
                titolo: titolo,
                ammontareOre: ammontareOre,
                descrizione: descrizione,
                costoDiRiferimento: costoRiferimento,
                livello: livello,
                progetto: progetto,
                categoria: categoria
            );
            CourseService.CreateCourse(c);
            return c;
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
