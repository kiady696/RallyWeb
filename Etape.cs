using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RallyWeb
{
    public class Etape
    {
        string idEtape;
        string nom;

        public Etape()
        {
        }

        public Etape(string idEtape, string nom)
        {
            IdEtape = idEtape;
            Nom = nom;
        }

        public string IdEtape { get => idEtape; set => idEtape = value; }
        public string Nom { get => nom; set => nom = value; }
    }
}