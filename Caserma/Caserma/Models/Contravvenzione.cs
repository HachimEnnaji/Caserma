using System;
using System.ComponentModel.DataAnnotations;

namespace Caserma.Models
{
    public class Contravvenzione
    {
        //classe contravvenzione con i dati anagrafici e le statistiche dopo il join
        public int ID_Anagrafica { get; set; }
        [Display(Name = "Cognome")]
        public string T_Cognome { get; set; }
        [Display(Name = "Nome")]
        public string T_Nome { get; set; }
        public string T_Indirizzo { get; set; }
        public string T_Citta { get; set; }
        public string T_CAP { get; set; }
        public string T_CodiceFiscale { get; set; }

        public int ID_Verbale { get; set; }
        public string V_NominativoAgente { get; set; }
        [Display(Name = "Data violazione")]
        public DateTime V_DataViolazione { get; set; }
        public string V_IndirizzoViolazione { get; set; }
        public DateTime V_DataTrascrizioneVerbale { get; set; }
        [Display(Name = "Importo")]
        public decimal V_Importo { get; set; }
        [Display(Name = "Totale Importo")]
        public decimal V_TotaleImporto { get; set; }
        [Display(Name = "Numero Verbali")]
        public int V_NumeroVerbali { get; set; }
        [Display(Name = "Decurtamento punti")]
        public int V_DecurtamentoPunti { get; set; }
        public int ID_Violazione { get; set; }
        public string V_Descrizione { get; set; }
    }
}