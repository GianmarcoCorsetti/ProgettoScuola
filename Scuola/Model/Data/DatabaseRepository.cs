using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using System.Data.SqlClient;
using System.Data;
using static Scuola.Model.Level;
using Scuola.Model;
using Scuola.Model.Data;

public class DatabaseRepository : IRepository
{
    const string CONNECTION_STRING = @"
            Server = localhost;
            User = sa;
            Password = 1Secure*Password;
            Database = gestione_corsi
         ";

    const string SELECT_COURSES = @"
            SELECT id, titolo, descrizione, ammontare_ore, costo_di_riferimento, id_livello, id_progetto, id_categoria
            FROM corsi
        ";
    
    const string ADD_AZIENDA = @"
            INSERT INTO corsi(id, nome, citta, indirizzo, cp, telefono, email, partita_iva)
            VALUES(@id, @nome, @citta, @indirizzo, @cp, @telefono, @email, @partita_iva)
        ";

    const string ADD_COURSE = @"
            INSERT INTO aziende(id, titolo, descrizione, ammontare_ore, corso_di_riferimento, id_livello, id_progetto, id_categoria)
            VALUES(@id, @titolo, @descrizione, @ammontare_ore, @corso_di_riferimento, @id_livello, @id_progetto, @id_categoria)
        ";

    const string SELECT_COURSE_EDITIONS =@"
        SELECT id, codice_edizione, data_inizio, data_fine, prezzo_finale, numero_max_studenti, in_presenza, id_aula, id_corso, id_finanziatore
        FROM edizioni
        WHERE id_corso = @id_corso
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
                                id: reader.GetInt64("id"),
                                titolo: reader.GetString("titolo"),
                                descrizione: reader.GetString("descrizione"),
                                ammontareOre: reader.GetInt32("ammontare_ore"),
                                costoDiRiferimento: reader.GetInt32("costo_di_riferimento"),
                                idLivello: reader.GetInt64("id_livello"),
                                idProgetto: reader.GetInt64("id_progetto"),
                                idCategoria: reader.GetInt64("id_categoria")
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

    public Corso AddCourse(Corso newCourse)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand(ADD_COURSE, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", 0);
                command.Parameters.AddWithValue("@titolo", newCourse.Titolo);
                command.Parameters.AddWithValue("@descrizione", newCourse.Descrizione);
                command.Parameters.AddWithValue("@ammontare_ore", newCourse.AmmontareOre);
                command.Parameters.AddWithValue("@costo_di_riferimento", newCourse.CostoDiRiferimento);
                command.Parameters.AddWithValue("@id_livello", newCourse.IdLivello);
                command.Parameters.AddWithValue("@id_progetto", newCourse.IdProgetto);
                command.Parameters.AddWithValue("@id_categoria", newCourse.IdCategoria);
                connection.Open();
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
                command.Parameters.AddWithValue("@id", 0);
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
                            id: reader.GetInt64("id"),
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
                            idAula: reader.GetInt64("id_aula"),
                            idCorso: reader.GetInt64("id_classe"),
                            idFinanziatore: reader.GetInt64("Id_finanziatore")
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

    public Corso FindById(long id)
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
                                    id: reader.GetInt64("id"),
                                    titolo: reader.GetString("titolo"),
                                    descrizione: reader.GetString("descrizione"),
                                    ammontareOre: reader.GetInt32("ammontare_ore"),
                                    costoDiRiferimento: reader.GetInt32("costo_di_riferimento"),
                                    idLivello: reader.GetInt64("id_livello"),
                                    idProgetto: reader.GetInt64("id_progetto"),
                                    idCategoria: reader.GetInt64("id_categoria")
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
                                    id: reader.GetInt64("id"),
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
}