using System;
using System.Collections;
using System.Data.SqlClient;

namespace RallyWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = DBSQLServerUtils.GetDBConnection();
            try
            {


                con.Open();
                ReflectsFunctions rf = new ReflectsFunctions();
                ArrayList al = rf.select(new Pilote(), "pilote", con, "");
                for (int i = 0; i < al.Count; i++)
                {
                    Pilote temp = (Pilote)al[i];
                    //remplissage liste pilotes
                    DropDownList1.Items.Insert(i, " " + temp.Nom + " " + temp.Temps);
                }

                /*remplissage liste etapes
                ArrayList ets = rf.select(new Etape(), "etape", con, "");
                for(int j = 0; j < ets.Count; j++)
                {
                    Etape oi = (Etape)ets[j];
                    ListBox1.Items.Insert(j, oi.Nom);
                }*/

                //remplissage liste Etape
                ArrayList etapes = rf.select(new Etape(), "etape", con, "");
                for(int m = 0; m < etapes.Count; m++)
                {
                    Etape temp = (Etape)etapes[m];
                    ListBox5.Items.Insert(m, "rally: " +temp.Nom);
                }

                //remplissage liste ES
                ArrayList ess = rf.select(new ES(), "ES", con, "");

                for (int k = 0; k < ess.Count; k++)
                {
                    ES io = (ES)ess[k];
                    ArrayList ets = rf.select(new Etape(), "etape", con, "where IdEtape=" + io.IdEtape);
                    Etape et = (Etape)ets[0];
                    ListBox2.Items.Insert(k, " rally " +" "+ et.Nom + ": " + io.Longueur + "km ");
                }

            }
            finally
            {
                con.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBSQLServerUtils.GetDBConnection();
            try {
                con.Open();

                //recuperation de l'epreuve speciale
                //getPosts?
                char[] sep = { ' ' };
                String es = (Request.Form["ListBox2"].Split(sep))[2];
                //getPilote de cette epreuve speciale
                ReflectsFunctions rf = new ReflectsFunctions();
                ArrayList clas = rf.select(new Pilote(), "pilote", con, "where idES=" + es.Substring(1,es.Length-2)); // (5)
                //classer ces pilotes
                Pilote[] pils = new Pilote[clas.Count];
                for(int i = 0; i < clas.Count; i++)
                {
                    Pilote temp = (Pilote)clas[i];
                    pils[i] = temp;
                }

                rf.classer(pils);
                //afficher ce classement
                for (int j = 0; j < pils.Length; j++) {
                    ListBox3.Items.Insert(j, ""+(j+1)+"è "+pils[j].Nom+" "+pils[j].Temps);
                }
                //tete de table: idES
                //nomPilote | temps | idVoiture
            }
            finally{
                con.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = DBSQLServerUtils.GetDBConnection();
            try
            {
                con.Open();
                ReflectsFunctions rf = new ReflectsFunctions();
                ArrayList pilotes = rf.select(new Pilote(), "genF", con, "ORDER BY tempstotal");
                char[] seps = { ' ' };
                for(int k = 0; k < pilotes.Count; k++)
                {
                    Pilote temp = (Pilote)pilotes[k];
                    String[] tp = temp.Tempstotal.ToString().Split(seps[0]);
                    ListBox4.Items.Insert(k, " " + temp.Nom + " " + tp[1] + "");
                }

            }
            finally
            {
                con.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //classement par etape
            SqlConnection con = DBSQLServerUtils.GetDBConnection();
            try
            {
                con.Open();
                ReflectsFunctions rf = new ReflectsFunctions();
                char[] sep = { ' ' };
                string etap = (Request.Form["ListBox5"].Split(sep))[1];
                ArrayList classmnt = rf.select(new Pilote(), "pilote", con, "where nomEtape=" + etap); //mila mapiasa vue maka ny idEtape av@ idES 
                //classer ces pilotes
                Pilote[] pils = new Pilote[classmnt.Count];
                for(int u = 0; u < classmnt.Count; u++)
                {
                    Pilote temp = (Pilote)classmnt[u];
                    pils[u] = temp;
                }
                rf.classer(pils);
                //afficher le classement par etape
                for(int z = 0; z < pils.Length; z++)
                {
                    ListBox6.Items.Insert(z, "" + (j + 1) + "è " + pils[j].Nom + " " + pils[j].Temps);
                }


            }
            finally
            {
                con.Close();
            }
        }
    }


}