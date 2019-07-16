using System;

namespace RallyWeb
{
    public class Pilote
    {
        String idPilote;
        String nom;
        String categorieP;
        String idVoiture;
        String idES;
        TimeSpan temps;
        DateTime tempstotal;
        int vitesseMoyenne;

        public Pilote()
        {
        }

        public Pilote(string idPilote, string nom, string categorieP, string idVoiture, string idES, TimeSpan temps)
        {
            this.IdPilote = idPilote;
            this.Nom = nom;
            this.CategorieP = categorieP;
            this.IdVoiture = idVoiture;
            this.IdES = idES;
            this.Temps = temps;
        }

        public string IdPilote { get => idPilote; set => idPilote = value; }
        public string Nom { get => nom; set => nom = value; }
        public string CategorieP { get => categorieP; set => categorieP = value; }
        public string IdVoiture { get => idVoiture; set => idVoiture = value; }
        public string IdES { get => idES; set => idES = value; }
        public TimeSpan Temps { get => temps; set => temps = value; }
        public DateTime Tempstotal { get => tempstotal; set => tempstotal = value; }
        public int VitesseMoyenne { get => vitesseMoyenne; set => vitesseMoyenne = value; } //calcul vitesse moyenne : distance / temps(m/s)
    }
}