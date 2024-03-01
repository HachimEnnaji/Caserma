using Caserma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Caserma.Controllers
{
    public class ContravvenzioneController : Controller
    {
        List<Contravvenzione> contravvenzioni = new List<Contravvenzione>();
        // GET: Contravvenzione
        public ActionResult Index()
        {
            return View();
        }

        // PARTIAL PAGES
        //parziale per visualizzare le contravvenzioni per trasgressore iniettate in Index Contrvavvenzione
        //con il totale importo e il numero di verbali
        //nessun parametro
        //return parziale con i dati richiesti
        public ActionResult _ContravvenzioniPerTrasgressore()
        {


            SqlConnection con = Connection.GetConnection();
            try
            {

                con.Open();
                string query = "SELECT Cognome, Nome, Sum(Importo) AS TotaleImporto, count(*) AS NumeroVerbali " +
                              "FROM ANAGRAFICA AS A join VERBALE AS V  ON A.IdAnagrafica = V.IdAnagrafica " +
                              "GROUP BY Cognome, Nome ORDER BY NumeroVerbali DESC";
                SqlDataReader reader = Connection.GetAdapter(query, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_TotaleImporto = Convert.ToDecimal(reader["TotaleImporto"]);
                        c.V_NumeroVerbali = Convert.ToInt32(reader["NumeroVerbali"]);

                        contravvenzioni.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniPerTrasgressore", contravvenzioni);
        }
        // PARTIAL PAGES
        //parziale per visualizzare le contravvenzioni che superano i 10 punti iniettate in Index Contrvavvenzione
        //nessun parametro
        //return parziale con i dati richiesti
        public ActionResult _ContravvenzioniSuperano10Punti()
        {

            SqlConnection con = Connection.GetConnection();
            try
            {
                con.Open();
                //join tra anagrafica e verbale per recuperare i dati richiesti
                string query = "SELECT Cognome, Nome, Importo, DataViolazione, DecurtamentoPunti " +
                    "FROM Anagrafica AS A join Verbale AS V  ON A.IdAnagrafica = V.IdAnagrafica  WHERE DecurtamentoPunti > 5 ";
                SqlDataReader reader = Connection.GetAdapter(query, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_Importo = Convert.ToDecimal(reader["Importo"]);
                        c.V_DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);


                        contravvenzioni.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniSuperano10Punti", contravvenzioni);
        }
        // PARTIAL PAGES
        //parziale per visualizzare le contravvenzioni con importo maggiore di 400 iniettate in Index Contrvavvenzione
        //nessun parametro
        //return parziale con i dati richiesti
        public ActionResult _ContravvenzioniImportoMaggiore400()
        {

            SqlConnection con = Connection.GetConnection();
            try
            {
                con.Open();
                string query = "SELECT Cognome, Nome, DataViolazione, DecurtamentoPunti, Importo FROM Anagrafica AS A join Verbale AS V  ON A.IdAnagrafica = V.IdAnagrafica  WHERE Importo > 400";
                SqlDataReader reader = Connection.GetAdapter(query, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_Importo = Convert.ToDecimal(reader["Importo"]);
                        c.V_DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);

                        contravvenzioni.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return PartialView("_ContravvenzioniImportoMaggiore400", contravvenzioni);
        }
        // PARTIAL PAGES
        //parziale per visualizzare i punti decurtati per ogni trasgressore iniettate in Index Contrvavvenzione
        //nessun parametro
        //return parziale con i dati richiesti
        public ActionResult _PuntiDecurtatiPerOgniTrasgressore()
        {

            SqlConnection con = Connection.GetConnection();
            try
            {
                con.Open();
                //join tra anagrafica e verbale per recuperare i dati richiesti
                string query = "SELECT Cognome, Nome, Sum(DecurtamentoPunti) AS DecurtamentoPunti" +
                    " FROM Anagrafica AS A  join Verbale AS V  ON A.IdAnagrafica = V.IdAnagrafica GROUP BY Cognome, Nome";
                SqlDataReader reader = Connection.GetAdapter(query, con);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Contravvenzione c = new Contravvenzione();
                        c.T_Cognome = reader["Cognome"].ToString();
                        c.T_Nome = reader["Nome"].ToString();
                        c.V_DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);

                        contravvenzioni.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return PartialView("_PuntiDecurtatiPerOgniTrasgressore", contravvenzioni);
        }
    }
}