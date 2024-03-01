using System.ComponentModel.DataAnnotations;

namespace Caserma.Models
{
    public class Anagrafica
    {
        //classe anagrafica con i dati anagrafici e le statistiche
        public int ID_Anagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto => $"{Nome} {Cognome}";
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        [Display(Name = "Cod Fiscale")]
        public string CodiceFiscale { get; set; }
        [Display(Name = "Violazioni")]
        public int TotaleViolazioni { get; set; }
        [Display(Name = "Punti decurtati")]
        public int TotaleDecurtamentoPunti { get; set; }
    }
}