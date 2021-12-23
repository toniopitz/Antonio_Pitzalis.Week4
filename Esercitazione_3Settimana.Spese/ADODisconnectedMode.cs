using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione_3Settimana.GestionaleSpese
{
    internal class ADODisconnectedMode
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static SqlDataAdapter InizializzaAdapter(SqlConnection connection)     
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("Select * from Spese");
            adapter.DeleteCommand = GeneraDeleteCommand(connection);
            return adapter;
        }

        private static SqlCommand GeneraDeleteCommand(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Spese where id=@id";

            cmd.Parameters.Add(new SqlParameter("@id",SqlDbType.Int,0,"id"));

            return cmd;
        }

        public static void DeleteSpesaById(int idSpesa)
        {
            DataSet speseDS = new DataSet();
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
                var speseAdapter = InizializzaAdapter(connection);
                speseAdapter.Fill(speseDS, "Spese");

                connection.Close();

                DataRow recordDaEliminare = speseDS.Tables[0].Rows.Find(idSpesa);
                if (recordDaEliminare != null)
                {
                    recordDaEliminare.Delete();
                }
                speseAdapter.Update(speseDS, "Spese");
                Console.WriteLine("Aggiornamento effettuato");
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
