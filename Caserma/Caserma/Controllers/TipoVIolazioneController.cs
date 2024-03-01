using Caserma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Caserma.Controllers
{
    public class TipoVIolazioneController : Controller
    {
        // GET: TipoVIolazione
        //index che stampa la lista
        //nessun parametro
        //return view con la lista dei tipi di violazione
        public ActionResult Index()
        {
            SqlConnection conn = Connection.GetConnection();
            List<TipoViolazione> tipiViolazione = new List<TipoViolazione>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM TIPO_VIOLAZIONE";
                SqlDataReader reader = Connection.GetAdapter(query, conn);
                while (reader.Read())
                {
                    TipoViolazione tipoViolazione = new TipoViolazione();
                    tipoViolazione.ID_Violazione = Convert.ToInt32(reader["IdViolazione"]);
                    tipoViolazione.Descrizione = reader["Descrizione"].ToString();
                    tipiViolazione.Add(tipoViolazione);
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
            return View(tipiViolazione);
        }
    }
}