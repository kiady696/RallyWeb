using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RallyWeb
{
    public class ES
    {
        string idES;
        int longueur;
        string idEtape;

        public ES()
        {
        }

        public ES(string idES, int longueur, string idEtape)
        {
            IdES = idES;
            Longueur = longueur;
            IdEtape = idEtape;
        }

        public string IdES { get => idES; set => idES = value; }
        public int Longueur { get => longueur; set => longueur = value; }
        public string IdEtape { get => idEtape; set => idEtape = value; }
    }
}