using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione_3Settimana.GestionaleSpese
{
    static class ADOConnectedMode
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        
        public static void GetAllCategoria()
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = "Select * From categoria;";
                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                Console.WriteLine("Elenco Categorie\n");
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var categoria = reader.GetString(1);
                    Console.WriteLine($"{id} - {categoria}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static void Insert(int idCat, DateTime dos, string desc, string utente,decimal importo)
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Creiamo la query
                string query = $"insert into Spese values('{dos}','{desc}','{utente}',{importo},0,{idCat})";
                //creiamo il comando
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e verifichiamo che sia andato tutto a buon fine
                int affected = cmd.ExecuteNonQuery();
                if(affected > 0)
                {
                    Console.WriteLine($"Inserimento avvenuto con successo per {affected} righe");
                }
                else
                {
                    Console.WriteLine("Qualcosa è andato storto");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void GetAllSpeseByCat()
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = $"Select categoria.categoria, count(*) as 'Numero Spese' From Spese inner join categoria on spese.idCategoria = categoria.id group by categoria.categoria; ;";

                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                Console.WriteLine("Numero di spese totali per categoria\n");
                while (reader.Read())
                {
                    var cate = reader.GetString(0);
                    var nSpese = reader.GetInt32(1);
                    
                    Console.WriteLine($"Categoria: {cate} - N°Spese {nSpese}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void GetAllSpeseByUtente(string utente)
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = $"Select * From Spese where utente like '{utente}';";
                
                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                Console.WriteLine("Elenco spese utente scelto\n");
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var dos = reader.GetDateTime(1);
                    var desc = reader.GetString(2);
                    var imp = reader.GetDecimal(4);
                    Console.WriteLine($"{id} - {desc} - Data {dos} - Importo {imp}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void GetAllUtenti()
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = "Select utente From Spese Group by utente;";
                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                Console.WriteLine("Elenco utenti con delle spese\n");
                while (reader.Read())
                {
                    var utente = reader.GetString(0);
                    Console.WriteLine($"- {utente}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void GetAllSpesaNonApprovate()
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = "Select * From Spese where spese.apporvato = 0;";
                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                Console.WriteLine("Elenco Spese\n");
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var descrizione = reader.GetString(2);
                    Console.WriteLine($"{id} - {descrizione}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void UpdateSpesaById(int idSpesa)
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Creiamo la query
                string query = $"update Spese set apporvato = 1 where Spese.id = {idSpesa}";
                //creiamo il comando
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e verifichiamo che sia andato tutto a buon fine
                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    Console.WriteLine($"La spesa è stata approvata");
                    
                }
                else
                {
                    Console.WriteLine("Qualcosa è andato storto");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void DeleteSpesaById(int idSpesa)
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Creiamo la query
                string query = $"Delete from Spese where Spese.id = {idSpesa}";
                //creiamo il comando
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e verifichiamo che sia andato tutto a buon fine
                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    Console.WriteLine($"La spesa è stata cancellata");

                }
                else
                {
                    Console.WriteLine("Qualcosa è andato storto");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void GetAllApproved()
        {
            using SqlConnection connection = new SqlConnection(connectionStringSQL);
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessione al DB stabilita");
                }
                else
                {
                    Console.WriteLine("Non è stato possibile connettersi al DB");
                }
                //Attribuiamo alla variabile query il comando desiderato
                string query = "select * from Spese where apporvato = 1;";
                //Creiamo il comando tramite la query e la connessione
                SqlCommand cmd = new SqlCommand(query, connection);
                //Eseguiamo e salviamo tutto su una variabile di tipo DataReader
                SqlDataReader reader = cmd.ExecuteReader();
                //Visualizziamo a schermo
                
                Console.WriteLine("Elenco Spese approvate\n");
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var descrizione = reader.GetString(2);
                    Console.WriteLine($"{id} - {descrizione}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
