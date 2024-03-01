using System;
using System.ComponentModel.DataAnnotations;

namespace Caserma.Models
{
    public class Verbale
    {

        //classe verbale con i dati anagrafici e le statistiche dopo il join
        [Display(Name = "Nr")]
        public int ID_Verbale { get; set; }
        public int ID_Anagrafica { get; set; }
        [Display(Name = "Agente")]
        public string NominativoAgente { get; set; }
        [Display(Name = "Data violazione")]
        public DateTime DataViolazione { get; set; }
        [Display(Name = "Indirizzo")]
        public string IndirizzoViolazione { get; set; }
        [Display(Name = "Data trascrizione")]
        public DateTime DataTrascrizioneVerbale { get; set; }
        [DataType(DataType.Currency)]
        public decimal Importo { get; set; }
        [Display(Name = "Decurtamento punti")]
        public int DecurtamentoPunti { get; set; }
        public int ID_Violazione { get; set; }
        [Display(Name = "TipoViolazione")]
        public string TipoViolazione { get; set; }


        [Display(Name = "Trasgressore")]
        public string NomeCognomeTrasgressore { get; set; }

    }
}