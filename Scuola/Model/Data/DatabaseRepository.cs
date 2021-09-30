using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using System.Data.SqlClient;
using System.Data;
using static Scuola.Model.Livello;
using Scuola.Model;
using Scuola.Model.Data;

public class DatabaseRepository : IRepository
{
    const string CONNECTION_STRING = @" Server = localhost; User = sa; Password = 1Secure*Password; Database = gestione_corsi ";

    const string SELECT_COURSES = @"
            SELECT id, titolo, descrizione, ammontare_ore, costo_di_riferimento, id_livello, id_progetto, id_categoria
            FROM corsi
        ";
        
    const string SELECT_AZIENDE = @"
            SELECT id, nome ,citta ,indirizzo, cp, telefono, email, partita_iva
            FROM aziende
        ";
        
    const string SELECT_PROGETTI = @"
            SELECT id, titolo, descrizione, id_azienda
            FROM progetti
        ";
        
    const string SELECT_CATEGORIE = @"
            SELECT id, tipo, descrizione
            FROM categorie
        ";
        
    const string SELECT_AULE = @"
            SELECT id, nome, capacita_max, fisica_virtuale, computerizzata, proiettore
            FROM corsi
        ";
        
    const string SELECT_LIVELLI = @"
            SELECT id, titolo, descrizione
            FROM livelli
        ";

    const string SELECT_FINANZIATORI = @"
            SELCECT id, titolo, descrizione
            FROM enti_finanziatori
        ";
    
    const string SELECT_COURSE_EDITIONS =@"
        SELECT id, codice_edizione, data_inizio, data_fine, prezzo_finale, numero_max_studenti, in_presenza, id_aula, id_corso, id_finanziatore
        FROM edizioni
        WHERE id_corso = @id_corso
    ";

    const string ADD_AZIENDA = @"
            INSERT INTO aziende(nome, citta, indirizzo, cp, telefono, email, partita_iva)
            VALUES(@nome, @citta, @indirizzo, @cp, @telefono, @email, @partita_iva)
        ";

    const string ADD_FINANZIATORE = @"
            INSERT INTO enti_finanziatori(titolo, descrizione)
            VALUES(@titolo, @descrizione)
        ";

    const string ADD_CATEGORIA = @"
            INSERT INTO categorie(tipo, descrizione)
            VALUES(@tipo, @descrizione)
        ";

    const string ADD_COURSE = @"
            INSERT INTO corsi(titolo, descrizione, ammontare_ore, costo_di_riferimento, id_livello, id_progetto, id_categoria)
            VALUES(@titolo, @descrizione, @ammontare_ore, @costo_di_riferimento, @id_livello, @id_progetto, @id_categoria)
        ";

    const string ADD_LIVELLO = @"
            INSERT INTO livelli(descrizione, tipo)
            VALUES(@descrizione, @tipo)
        ";
    const string ADD_AULA = @"
            INSERT INTO  aule( nome, capacita_max, fisica_virtuale, computerizzata, proiettore)
            VALUES ( @nome, @capacita_max, @fisica_virtuale, @computerizzata, @proiettore)
        ";
    
    const string ADD_PROGETTO = @"
            INSERT INTO progetti(titolo, descrizione, id_azienda)
            VALUES(@titolo, @descrizione, @id_azienda)
        ";

    const string ADD_COURSE_EDITION = @"
            INSERT INTO edizioni(id, codice_edizione, data_inizio, data_fine, prezzo_finale, numero_max_studenti, in_presenza, id_aula, id_corso, id_finanziatore)
            VALUES(@id, @codice_edizione, @data_inizio, @data_fine, @prezzo_finale, @numero_max_studenti, @in_presenza, @id_aula, @id_corso, @id_finanziatore)
        ";

    const string SELECT_COURSE_ID = @"
            SELECT id, titolo, descrizione, ammontare_ore, costo_di_riferimento, id_livello, id_progetto, id_categoria
            FROM corsi
            WHERE id = @id
        ";
    const string SELECT_AZIENDA_ID = @"
            SELECT id, nome, citta, indirizzo, cp, telefono, email, partita_iva
            FROM aziende
            WHERE id = @id
        ";
    const string SELECT_LIVELLO_ID = @"
            SELECT id ,descrizione, tipo
            FROM livelli
            WHERE id = @id
        ";
    const string SELECT_CATEGORIA_ID = @"
            SELECT id ,descrizione, tipo
            FROM categoria
            WHERE id = @id
        ";
        
    const string SELECT_PROGETTO_ID = @"
            SELECT id, titolo, descrizione, id_azienda
            FROM progetti
            WHERE id = @id
        ";
        
    const string SELECT_AULA_ID = @"
            SELECT id, nome, capacita_max, fisica_virtuale, computerizzata, proiettore
            FROM aule
            WHERE id = @id
        ";

    const string SELECT_FINANZIATORE_ID = @"
            SELECT id, titolo, descrizione
            FROM enti_finanziatori
            WHERE id = @id
        ";

    const string SELECT_COURSE_EDITION_ID = @"
            SEELCT id, codice_edizione, data_inizio, data_fine, prezzo_finale, numero_max_studenti, in_presenza, id_aula, id_corso, id_finanziatore
            FROM edizioni
            WHERE id = @id
        ";


    public IEnumerable<Corso> GetCourses()
    {
        List<Corso> corsi = new List<Corso>();
        try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(SELECT_COURSES, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Corso corso = new Corso(
                                id: reader.GetInt32("id"),
                                titolo: reader.GetString("titolo"),
                                descrizione: reader.GetString("descrizione"),
                                ammontareOre: reader.GetInt32("ammontare_ore"),
                                costoDiRiferimento: reader.GetDecimal("costo_di_riferimento"),
                                idLivello: reader.GetInt32("id_livello"),
                                idProgetto: reader.GetInt32("id_progetto"),
                                idCategoria: reader.GetInt32("id_categoria")
                            );
                        corsi.Add(corso);
                        }
                    }
                }
                return corsi;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
    }
    public IEnumerable<Azienda> GetAziendas()
    {
        List<Azienda> aziende = new List<Azienda>();
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_AZIENDE, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // id, nome ,citta ,indirizzo, cp, telefono, email, partita_iva
                        Azienda azienda = new Azienda(
                            id: reader.GetInt32("id"),
                            nome: reader.GetString("nome"),
                            citta: reader.GetString("citta"),
                            indirizzo: reader.GetString("indirizzo"),
                            cP: reader.GetString("cp"),
                            telefono: reader.GetString("telefono"),
                            email: reader.GetString("email"),
                            partitaIva: reader.GetString("partita_iva")
                        );
                        aziende.Add(azienda);
                    }
                }
            }
            return aziende;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }
    public IEnumerable<Progetto> GetProgettos()
    {
        List<Progetto> progetti = new List<Progetto>();
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_PROGETTI, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Progetto progetto = new Progetto(
                            id: reader.GetInt32("id"),
                            titolo: reader.GetString("titolo"),
                            descrizione: reader.GetString("descrizione"),
                            idAzienda: reader.GetInt32("id_azienda")
                        );
                        progetti.Add(progetto);
                    }
                }
            }
            return progetti;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }
    public IEnumerable<Categoria> GetCategorias()
    {
        List<Categoria> categorie = new List<Categoria>();
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_CATEGORIE, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Enum.TryParse(reader.GetString("tipo"), out Category category); // da problemi
                        Categoria categoria = new Categoria(
                            id: reader.GetInt32("id"),
                            descrizione: reader.GetString("descrizione"),
                            categoriaCorso: category
                        );
                        categorie.Add(categoria);
                    }
                }
            }
            return categorie;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }
    public IEnumerable<Livello> GetLivellos()
    {
        List<Livello> livelli = new List<Livello>();
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_LIVELLI, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Enum.TryParse(reader.GetString("tipo"), out ExperienceLevel level);
                        Livello livello = new Livello(
                            id: reader.GetInt32("id"),
                            descrizione: reader.GetString("descrizione"),
                            livelloCorso: level
                        );
                        livelli.Add(livello);
                    }
                }
            }
            return livelli;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Corso AddCourse(Corso newCourse)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_COURSE, connection);
                connection.Open();
                command.Parameters.AddWithValue("@titolo", newCourse.Titolo);
                command.Parameters.AddWithValue("@descrizione", newCourse.Descrizione);
                command.Parameters.AddWithValue("@ammontare_ore", newCourse.AmmontareOre);
                command.Parameters.AddWithValue("@costo_di_riferimento", newCourse.CostoDiRiferimento);
                command.Parameters.AddWithValue("@id_livello", newCourse.IdLivello);
                command.Parameters.AddWithValue("@id_progetto", newCourse.IdProgetto);
                command.Parameters.AddWithValue("@id_categoria", newCourse.IdCategoria);
                command.ExecuteNonQuery();
            }
            return newCourse;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Azienda AddAzienda(Azienda newAzienda)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_AZIENDA, connection);
                connection.Open();
                command.Parameters.AddWithValue("@nome", newAzienda.Nome);
                command.Parameters.AddWithValue("@citta", newAzienda.Citta);
                command.Parameters.AddWithValue("@indirizzo", newAzienda.Indirizzo);
                command.Parameters.AddWithValue("@cp", newAzienda.CP);
                command.Parameters.AddWithValue("@telefono", newAzienda.Telefono);
                command.Parameters.AddWithValue("@email", newAzienda.Email);
                command.Parameters.AddWithValue("@partita_iva", newAzienda.PartitaIva);
                command.ExecuteNonQuery();
            }
            return newAzienda;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Livello AddLivello(Livello newLivello)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_LIVELLO, connection);
                connection.Open();
                command.Parameters.AddWithValue("@descrizione", newLivello.Descrizione);
                command.Parameters.AddWithValue("@tipo", newLivello.LivelloCorso.ToString());
                command.ExecuteNonQuery();
            }
            return newLivello;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Categoria AddCategoria(Categoria newCategoria)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_CATEGORIA, connection);
                connection.Open();
                command.Parameters.AddWithValue("@descrizione", newCategoria.Descrizione);
                command.Parameters.AddWithValue("@tipo", newCategoria.CategoriaCorso.ToString());
                command.ExecuteNonQuery();
            }
            return newCategoria;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Progetto AddProgetto(Progetto newProgetto)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_PROGETTO, connection);
                connection.Open();
                command.Parameters.AddWithValue("@descrizione", newProgetto.Descrizione);
                command.Parameters.AddWithValue("@titolo", newProgetto.Titolo); 
                command.Parameters.AddWithValue("@id_azienda", newProgetto.IdAzienda);
                command.ExecuteNonQuery();
            }
            return newProgetto;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }
    
    public IEnumerable<EdizioneCorso> GetCourseEditions(long courseId)
    {
        List<EdizioneCorso> edizioni = new List<EdizioneCorso>();
        try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    SqlCommand command = new SqlCommand(SELECT_COURSE_EDITIONS, connection);
                    command.Parameters.AddWithValue("@id_corso", courseId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        EdizioneCorso edizioneCorso = new EdizioneCorso(
                            id: reader.GetInt32("id"),
                            codiceEdizione: reader.GetString("codice_edizione"),
                            start: new LocalDate(
                                    reader.GetDateTime("data_inizio").Year,
                                    reader.GetDateTime("data_inizio").Month,
                                    reader.GetDateTime("data_inizio").Day
                                ),
                            end: new LocalDate(
                                    reader.GetDateTime("data_fine").Year,
                                    reader.GetDateTime("data_fine").Month,
                                    reader.GetDateTime("data_fine").Day
                                ),
                            maxStudenti: reader.GetInt32("numero_max_studenti"),
                            realPrice: reader.GetDecimal("prezzo_finale"),
                            inPresenza: reader.GetBoolean("in_presenza"),
                            idAula: reader.GetInt32("id_aula"),
                            idCorso: reader.GetInt32("id_classe"),
                            idFinanziatore: reader.GetInt32("Id_finanziatore")
                            );
                        edizioni.Add(edizioneCorso);
                        }
                    }
                }
                return edizioni;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
    }

    //id, codice_edizione, data_inizio, data_fine, prezzo_finale, numero_max_studenti, in_presenza, id_categoria
    public EdizioneCorso AddEdition(EdizioneCorso newCourseEdition)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_COURSE_EDITION, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", newCourseEdition.Id);
                command.Parameters.AddWithValue("@codice_edizione", newCourseEdition.CodiceEdizione);
                command.Parameters.AddWithValue("@data_inizio", newCourseEdition.Start);
                command.Parameters.AddWithValue("@data_fine", newCourseEdition.End);
                command.Parameters.AddWithValue("@prezzo_finale", newCourseEdition.RealPrice);
                command.Parameters.AddWithValue("@numero_max_studenti", newCourseEdition.MaxStudenti);
                command.Parameters.AddWithValue("@in_presenza", newCourseEdition.InPresenza);
                command.Parameters.AddWithValue("@id_aula", newCourseEdition.IdAula);
                command.Parameters.AddWithValue("@id_corso", newCourseEdition.IdCorso);
                command.Parameters.AddWithValue("@id_finanziatore", newCourseEdition.IdFinanziatore);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return newCourseEdition;
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error: " + e.Message);
            return null;
        }
    }

    public Corso FindCourseById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_COURSE_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Corso(
                                    id: reader.GetInt32("id"),
                                    titolo: reader.GetString("titolo"),
                                    descrizione: reader.GetString("descrizione"),
                                    ammontareOre: reader.GetInt32("ammontare_ore"),
                                    costoDiRiferimento: reader.GetInt32("costo_di_riferimento"),
                                    idLivello: reader.GetInt32("id_livello"),
                                    idProgetto: reader.GetInt32("id_progetto"),
                                    idCategoria: reader.GetInt32("id_categoria")
                                    );
                    }
                    return null;
                }
            }
        }
        catch(SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Azienda FindAziendaById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_AZIENDA_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Azienda(
                                    id: reader.GetInt32("id"),
                                    nome: reader.GetString("nome"),
                                    citta: reader.GetString("citta"),
                                    indirizzo: reader.GetString("indirizzo"),
                                    cP: reader.GetString("cp"),
                                    telefono: reader.GetString("telefono"),
                                    email: reader.GetString("email"),
                                    partitaIva: reader.GetString("partita_iva")
                                    );
                    }
                    return null;
                }
            }
        }
        catch(SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Livello FindLivelloById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_LIVELLO_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Enum.TryParse(reader.GetString("tipo"), out ExperienceLevel livelloCorso);
                        return new Livello(
                                    id: reader.GetInt32("id"),
                                    descrizione: reader.GetString("descrizione"),
                                    livelloCorso: livelloCorso
                                    );
                    }
                    return null;
                }
            }
        }
        catch(SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Progetto FindProgettoById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_PROGETTO_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Progetto(
                                    id: reader.GetInt32("id"),
                                    descrizione: reader.GetString("descrizione"),
                                    titolo: reader.GetString("titolo"),
                                    idAzienda: reader.GetInt32("id_azienda")
                                    );
                    }
                    return null;
                }
            }
        }
        catch(SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Categoria FindCategoriaById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_CATEGORIA_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Enum.TryParse(reader.GetString("tipo"), out Category categoriaCorso);
                        return new Categoria(
                                    id: reader.GetInt32("id"),
                                    categoriaCorso: categoriaCorso,
                                    descrizione: reader.GetString("descrizione")
                                    );
                    }
                    return null;
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Aula FindAulaById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_AULA_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Aula(
                                    id: reader.GetInt32("id"),
                                    nome: reader.GetString("nome"),
                                    capacitaMax: reader.GetInt32("capacita_max"),
                                    virtuale: reader.GetBoolean("fisica_viruale"),
                                    isComputerized: reader.GetBoolean("computerizzata"),
                                    hasProjector: reader.GetBoolean("proiettore")
                                    );
                    }
                    return null;
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }
    public Finanziatore FindFinanziatoreById(long id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(SELECT_FINANZIATORE_ID, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Finanziatore(
                                    id: reader.GetInt32("id"),
                                    titolo: reader.GetString("titolo"),
                                    descrizione: reader.GetString("descrizione")
                                    );
                    }
                    return null;
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine("Errore: " + e.Message);
            return null;
        }
    }

    public IEnumerable<Aula> GetAulas()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Finanziatore> GetFinanziatores()
    {
        throw new NotImplementedException();
    }

    public Aula AddAula(Aula newAula)
    {
        throw new NotImplementedException();
    }

    public Finanziatore AddFinanziatore(Finanziatore newFinanziatore)
    {
        throw new NotImplementedException();
    }
}