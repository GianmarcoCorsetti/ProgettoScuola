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
                        CreateCourse();
                        break;
                    case 'c':
                        ShowCourseEditionsByCourse();
                        break;
                    case 'd':
                        CreateCourseEdition();
                        break;
                    case 'e':
                        ShowStatisticalReport();
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
        public void CreateCourseEdition(){ // Da fare
            long idEdition = ReadLong("Inserire l'Id della edizione del corso : "); // ok
            //long idCourse = ReadLong("Inserire l'Id del corso : "); questo non va bene perché dobbiamo creare un Corso

            LocalDate start = ReadLocalDate("Inserire la data di inizio del corso (yyyy-mm-dd) : ");
            LocalDate finish = ReadLocalDate("Inserire la data di fine del corso (yyyy-mm-dd) : ");
            int numStudents = (int)ReadLong("Inserire il numero di studenti che seguiranno il corso: ");
            decimal realPrice = ReadDecimal("Inserire il prezzo finale dell'edizione del corso : ");
            var edition = new EdizioneCorso(id: idEdition, classCorso: null, start: start, end: finish, numStudents: numStudents, realPrice: realPrice);
            if(CourseService.CreateCourseEdition(edition, idCourse) == null){
                WriteLine(DIVISORE);
                WriteLine("Impossibile aggiungere edizioni con lo stesso ID");
            }else{
                CourseService.CreateCourseEdition(edition, idCourse);
                Console.Clear();
                WriteLine("Edizione Inserita con successo");
            }
        }
        private void ShowCourses(){ // OK 
            IEnumerable<Corso> courses = CourseService.GetAllCourses();
            foreach (var c in courses){
                WriteLine(c.ToString());
            }
        }
        private void CreateCourse(){ // Da fare
            long id = ReadLong("Inserisci l'id del corso: "); // ok
            string titolo = ReadString("Inserisci il titolo del corso: "); // ok 
            int ammontareOre = (int)ReadLong("Inserisci il numero di ore del corso: ");// ok
            string descrizione = ReadString("Inserisci una descrizione del corso: ");// ok
            decimal costoRiferimento = ReadDecimal("Inserisci il costo di riferimento del corso: ");// ok
            string livelloString = ReadString("Inserire il livello del corso tra le seguenti : " +
                "PRINCIPIANTE - MEDIO - ESPERTO - GURU: ");
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
            int durata = (int)ReadLong("Inserisci la durata del corso");
            decimal prezzo = ReadDecimal("Inserisci il prezzo del corso");
            Corso c = new Corso(
                newId: id,
                newTitolo: titolo,
                newDurataInOre: durata,
                newLevel: level,
                newDescription: descrizione,
                newStandardPrice: prezzo
                );
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
