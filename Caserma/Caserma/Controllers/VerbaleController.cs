using Caserma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PoliziaMunicipale_GestioneContravvenzioni.Controllers
{
    public class VerbaleController : Controller
    {
        // GET: Verbale
        //index che stampa la lista completa dei verbali usando come model Verbale, classe usata con i dati del join delle due tabelle
        //nessun parametro
        //return view con la lista dei verbali
        public ActionResult Index()
        {
            List<Verbale> ListaVerbali = new List<Verbale>();
            SqlConnection conn = Connection.GetConnection();
            try
            {

                conn.Open();
                //query per recuperare i dati dei verbali con il join delle tabelle
                string query = "SELECT * FROM Verbale AS V JOIN TIPO_VIOLAZIONE AS tV " +
                    "ON tV.IdViolazione = V.IdViolazione JOIN ANAGRAFICA AS A ON A.IdAnagrafica = V.IdAnagrafica " +
                    "ORDER BY DataTrascrizioneVerbale DESC";
                SqlDataReader reader = Connection.GetAdapter(query, conn);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //creazione di un oggetto verbale e popolamento con i dati del reader
                        Verbale v = new Verbale();
                        v.ID_Verbale = Convert.ToInt32(reader["IdVerbale"]);
                        v.NominativoAgente = reader["Nominativo_Agente"] + "";
                        v.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                        v.IndirizzoViolazione = reader["IndirizzoViolazione"] + "";
                        v.DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]);
                        v.Importo = Convert.ToDecimal(reader["importo"]);
                        v.DecurtamentoPunti = Convert.ToInt32(reader["DecurtamentoPunti"]);
                        v.ID_Violazione = Convert.ToInt32(reader["IdViolazione"]);
                        v.ID_Anagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                        v.TipoViolazione = reader["Descrizione"] + "";
                        v.NomeCognomeTrasgressore = reader["Nome"] + " " + reader["Cognome"];
                        //aggiunta dell'oggetto verbale alla lista
                        ListaVerbali.Add(v);

                    }
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
            //ritorno della view con la lista dei verbali
            return View(ListaVerbali);
        }
        //action per la creazione di un nuovo verbale
        //nessun parametro
        //fa due select a cascata per popolare le dropdownlist con dati delle tabelle anagrafica e tipo violazione
        //return view con le dropdownlist popolate e pronte per la creazione del verbale
        public ActionResult Archivia()
        {
            SqlConnection conn = Connection.GetConnection();
            List<TipoViolazione> listaViolazioni = new List<TipoViolazione>();
            try
            {
                conn.Open();
                //query per recuperare i dati dei tipi di violazione
                string query = "SELECT * FROM TIPO_VIOLAZIONE";
                SqlDataReader reader = Connection.GetAdapter(query, conn);

                //se il reader ha righe
                if (reader.HasRows)
                {
                    //ciclo sul reader
                    while (reader.Read())
                    {
                        //creazione di un oggetto tipo violazione e popolamento con i dati del reader
                        TipoViolazione t = new TipoViolazione();
                        t.ID_Violazione = Convert.ToInt32(reader["IdViolazione"]);
                        t.Descrizione = reader["Descrizione"].ToString();
                        listaViolazioni.Add(t);

                    }
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
            //creazione di una lista di anagrafiche
            SqlConnection con2 = Connection.GetConnection();
            List<Anagrafica> listaTrasgressoriCercati = new List<Anagrafica>();
            try
            {
                con2.Open();
                //query per recuperare i dati delle anagrafiche
                string query = $"SELECT * FROM ANAGRAFICA ";
                SqlDataReader reader = Connection.GetAdapter(query, con2);


                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        //creazione di un oggetto anagrafica e popolamento con i dati del reader
                        Anagrafica a = new Anagrafica();
                        a.ID_Anagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                        a.Cognome = reader["Cognome"].ToString();
                        a.Nome = reader["Nome"].ToString();
                        a.Indirizzo = reader["Indirizzo"].ToString();
                        a.Citta = reader["Città"].ToString();
                        a.CAP = reader["CAP"].ToString();
                        a.CodiceFiscale = reader["Cod_Fisc"].ToString();
                        listaTrasgressoriCercati.Add(a);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con2.Close();
            }
            //popolamento della viewbag con i dati delle dropdownlist
            ViewBag.TipoViolazioni = listaViolazioni;
            ViewBag.ListaTrasgressoriCercati = listaTrasgressoriCercati;
            return View();

        }
        //action per la creazione del verbale
        //parametro verbale, oggetto verbale da creare
        //return redirect alla index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Archivia(Verbale verbale)
        {
            //se il modelstate è valido
            if (ModelState.IsValid)
            {
                SqlConnection conn = Connection.GetConnection();
                try
                {
                    conn.Open();
                    //query per l'inserimento dei dati del verbale
                    string query = "INSERT INTO VERBALE (IdAnagrafica,IdViolazione,DataViolazione,IndirizzoViolazione,Nominativo_Agente,DataTrascrizioneVerbale,Importo,DecurtamentoPunti) " +
                        "VALUES (@ID_Anagrafica,@ID_Violazione,@DataViolazione,@IndirizzoViolazione,@NominativoAgente,@DataTrascrizione,@Importo,@DecurtamentoPunti)";
                    //comando per l'inserimento dei dati del verbale
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_Anagrafica", verbale.ID_Anagrafica);
                    cmd.Parameters.AddWithValue("@ID_Violazione", verbale.ID_Violazione);
                    cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    cmd.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                    cmd.Parameters.AddWithValue("@DataTrascrizione", verbale.DataTrascrizioneVerbale);
                    cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);

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
            }
            //redirect alla index
            return RedirectToAction("Index");
        }

    }
}