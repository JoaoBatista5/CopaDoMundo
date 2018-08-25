using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CopaDoMundo.Models
{
    public class Jogos
    {
        private int Id_Jogo { get; set; }
        private int Id_Chave { get; set; }
        private int Id_Time1 { get; set; }
        private int SalGols1 { get; set; }
        private int Id_Time2 { get; set; }
        private int SalGols2 { get; set; }
        private bool Empatou { get; set; }
        private int SalPenalteTime1 { get; set; }
        private int SalPenalteTime2 { get; set; }
    }
}