using Caserma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Caserma.Controllers
{
    public class AnagraficaController : Controller
    {
        // GET: Anagrafica
        public ActionResult Index()
        {
            List<Anagrafica> anagrafiche = new List<Anagrafica>();
            SqlConnection conn = Connection.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Anagrafica";
                SqlDataReader reader = Connection.GetAdapter(query, conn);
                while (reader.Read())
                {
                    Anagrafica anagrafica = new Anagrafica();
                    anagrafica.ID_Anagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                    anagrafica.Cognome = reader["Cognome"].ToString();
                    anagrafica.Nome = reader["Nome"].ToString();
                    anagrafica.Indirizzo = reader["Indirizzo"].ToString();
                    anagrafica.Citta = reader["Città"].ToString();
                    anagrafica.CAP = reader["CAP"].ToString();
                    anagrafica.CodiceFiscale = reader["Cod_Fisc"].ToString();
                    anagrafiche.Add(anagrafica);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(anagrafiche);
        }
        // edit per popolare la view con i dati dell'anagrafica da modificare
        //parametro id per recuperare l'anagrafica da modificare
        //return view con i dati dell'anagrafica da modificare
        public ActionResult Edit(int? id)
        {
            SqlConnection conn = Connection.GetConnection();
            Anagrafica anagrafica = new Anagrafica();
            try
            {
                conn.Open();
                string query = $"SELECT * FROM Anagrafica WHERE IdAnagrafica = {id}";
                SqlDataReader reader = Connection.GetAdapter(query, conn);
                while (reader.Read())
                {
                    anagrafica.ID_Anagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                    anagrafica.Cognome = reader["Cognome"].ToString();
                    anagrafica.Nome = reader["Nome"].ToString();
                    anagrafica.Indirizzo = reader["Indirizzo"].ToString();
                    anagrafica.Citta = reader["Città"].ToString();
                    anagrafica.CAP = reader["CAP"].ToString();
                    anagrafica.CodiceFiscale = reader["Cod_Fisc"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(anagrafica);
        }
        // edit per modificare i dati
        //parametro anagrafica per recuperare i dati modificati
        //returrn view index con i dati modificati e lista aggiornata
        [HttpPost]
        public ActionResult Edit(Anagrafica anagrafica)
        {
            SqlConnection conn = Connection.GetConnection();
            try
            {
                conn.Open();

                string query = "UPDATE Anagrafica SET Cognome = @Cognome, Nome = @Nome, Indirizzo = @Indirizzo, Città = @Città, CAP = @CAP, Cod_Fisc = @CodiceFiscale WHERE IdAnagrafica = @ID_Anagrafica";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                cmd.Parameters.AddWithValue("@Città", anagrafica.Citta);
                cmd.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                cmd.Parameters.AddWithValue("@CodiceFiscale", anagrafica.CodiceFiscale);
                cmd.Parameters.AddWithValue("@ID_Anagrafica", anagrafica.ID_Anagrafica);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        //create vuoto per popolare la view con i dati dell'anagrafica da inserire
        //nessun parametro
        //return view con i dati dell'anagrafica da inserire
        public ActionResult Create()
        {
            return View();
        }

        //create per inserire i dati
        //parametro anagrafica per recuperare i dati da inseriti nel create get
        //return view index con i dati inseriti e lista aggiornata
        [HttpPost]
        public ActionResult Create(Anagrafica anagrafica)
        {
            SqlConnection conn = Connection.GetConnection();
            try
            {
                conn.Open();
                string query = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES ('" + anagrafica.Cognome + "', '" + anagrafica.Nome + "', '" + anagrafica.Indirizzo + "', '" + anagrafica.Citta + "', '" + anagrafica.CAP + "', '" + anagrafica.CodiceFiscale + "')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }
    }
}