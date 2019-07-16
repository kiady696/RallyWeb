using System;
using System.Collections;
using System.Data.SqlClient;
using System.Reflection;



public class ReflectsFunctions
{
    public void genArray(ArrayList arrayl, Object[] tableO)
    {

        for (int i = 0; i < arrayl.Count; i++)
        {
            tableO[i] = arrayl[i];
        }

    }
    public object[] tableNames(SqlConnection c)
    {
        ArrayList list = new ArrayList();
        string sql = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME NOT LIKE '%Profil%' and TABLE_NAME NOT LIKE '%Utilisateur%' and TABLE_NAME NOT LIKE '%SuperUser%' ";
        SqlCommand command = new SqlCommand(sql, c);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(reader.GetString(0));
        }
        reader.Close();
        object[] listTable = list.ToArray();
        return listTable;
    }

    public void classer(RallyWeb.Pilote[] aclasser)
    {
        for(int i = 0; i < aclasser.Length; i++)
        {
            for(int j = 0; j < i; j++)
            {
                if (aclasser[j].Temps.CompareTo(aclasser[i].Temps) == 1) //ra inferieur ny temps anle faharoa
                {
                    RallyWeb.Pilote temp = aclasser[j];
                    aclasser[j] = aclasser[i];
                    aclasser[i] = temp;
                }
            }
        }
    }



    public object[] columnNames(SqlConnection c, string tablename)
    {
        string sql = "select column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + tablename + "'";
        ArrayList list = new ArrayList();
        SqlCommand command = new SqlCommand(sql, c);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(reader.GetString(0));
        }
        reader.Close();
        object[] listCol = list.ToArray();
        return listCol;
    }


    public bool isPropertyInTable(object[] cols, string name)
    {
        bool b = false;
        foreach (string col in cols)
        {
            if (name.ToUpper().CompareTo(col.ToUpper()) == 0)
            {
                b = true;
                break;
            }
        }
        return b;
    }

    public string reformat(string date)
    {
        string dt = "";
        char[] separators = new char[] { '/', '-' };
        string[] dts = date.Split(separators);
        if (dts.Length == 3)
        {
            if (dts[2].Length == 4)
            {
                string temp = dts[2];
                dts[2] = dts[0];
                dts[0] = temp;
            }
        }
        else
        {
            throw new Exception("22 - format invalide, format:YYYY/MM/DD");
        }
        for (int i = 0; i < 3; i++)
        {
            dt += "/" + dts[i];
        }
        dt = dt.Substring(1, dt.Length - 1);
        return dt;
    }

    int CompareToIgnoreCase(String arg1, String arg2)
    {
        return arg1.ToUpper().CompareTo(arg2.ToUpper());
    }


    public ArrayList select(object o, string tablename, SqlConnection connection, string conditions)
    {
        string rqt = "select * from " + tablename + " " + conditions;

        SqlCommand command = new SqlCommand(rqt, connection);
        SqlDataReader reader = command.ExecuteReader();
        ArrayList list = new ArrayList();
        try
        {

            int nbColumn = reader.FieldCount;
            while (reader.Read())
            {
                //Colonne i == Attribut j -> appel set
                object element = Activator.CreateInstance(o.GetType());
                System.Reflection.PropertyInfo[] p_infos = element.GetType().GetProperties();
                for (int i = 0; i < nbColumn; i++)
                {
                    foreach (System.Reflection.PropertyInfo p_info in p_infos)
                    {
                        if (CompareToIgnoreCase(p_info.Name, reader.GetName(i)) == 0)
                        {
                            System.Reflection.MethodInfo[] setAndGet = p_info.GetAccessors();

                            object[] args = null;
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.Int32") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetInt32(i) };

                            }
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.String") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetString(i) };
                            }
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.DateTime") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetDateTime(i) };
                            }
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.TimeSpan") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetTimeSpan(i) };
                            }
                            foreach (System.Reflection.MethodInfo fct in setAndGet)
                            {
                                if (fct.ReturnType == typeof(void) && args != null)
                                {
                                    fct.Invoke(element, args);
                                }
                            }

                        }
                    }
                }

                list.Add(element);
            }
            reader.Close();
        }
        catch (NotImplementedException ie)
        {
            throw ie;
        }
        return list;
    }

    public ArrayList selectDistinctUser(object o, string tablename, SqlConnection connection, string conditions)
    {
        string rqt = "select Distinct idUser from " + tablename + " " + conditions;

        SqlCommand command = new SqlCommand(rqt, connection);
        SqlDataReader reader = command.ExecuteReader();
        ArrayList list = new ArrayList();
        try
        {

            int nbColumn = reader.FieldCount;
            while (reader.Read())
            {
                //Colonne i == Attribut j -> appel set
                object element = Activator.CreateInstance(o.GetType());
                System.Reflection.PropertyInfo[] p_infos = element.GetType().GetProperties();
                for (int i = 0; i < nbColumn; i++)
                {
                    foreach (System.Reflection.PropertyInfo p_info in p_infos)
                    {
                        if (CompareToIgnoreCase(p_info.Name, reader.GetName(i)) == 0)
                        {
                            System.Reflection.MethodInfo[] setAndGet = p_info.GetAccessors();

                            object[] args = null;
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.Int32") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetInt32(i) };

                            }
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.String") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetString(i) };
                            }
                            if (CompareToIgnoreCase(reader[i].GetType().ToString(), "System.DateTime") == 0 && (!reader.IsDBNull(i)))
                            {
                                args = new object[] { reader.GetDateTime(i) };
                            }
                            foreach (System.Reflection.MethodInfo fct in setAndGet)
                            {
                                if (fct.ReturnType == typeof(void) && args != null)
                                {
                                    fct.Invoke(element, args);
                                }
                            }

                        }
                    }
                }

                list.Add(element);
            }
            reader.Close();
        }
        catch (NotImplementedException ie)
        {
            throw ie;
        }
        return list;
    }

    public string insertQuery(object o, string tablename, object[] columns)
    {
        string sql = "insert into " + tablename + " values (";
        System.Reflection.PropertyInfo[] o_properties = o.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo property in o_properties)
        {
            if (isPropertyInTable(columns, property.Name))
            {
                System.Reflection.MethodInfo[] setAndGet = property.GetAccessors();
                if (setAndGet[0].ReturnType != typeof(void))
                {
                    if (setAndGet[0].ReturnType == typeof(string))
                    {
                        sql += "'" + setAndGet[0].Invoke(o, null) + "'";
                        sql += ",";
                    }
                    if (setAndGet[0].ReturnType == typeof(int))
                    {
                        sql += setAndGet[0].Invoke(o, null).ToString();
                        sql += ",";
                    }
                }
            }
        }
        sql = sql.Remove(sql.Length - 1);
        sql += ")";

        return sql;
    }
    public string updateQuery(object o, string tablename, string conditions, object[] columns)
    {
        string sql = "update " + tablename + " set ";
        System.Reflection.PropertyInfo[] o_properties = o.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo property in o_properties)
        {
            if (isPropertyInTable(columns, property.Name))
            {
                System.Reflection.MethodInfo[] setAndGet = property.GetAccessors();
                sql += property.Name + "=";
                if (setAndGet[0].ReturnType != typeof(void))
                {
                    if (setAndGet[0].ReturnType == typeof(string))
                    {
                        sql += "'" + setAndGet[0].Invoke(o, null) + "'";
                    }
                    if (setAndGet[0].ReturnType == typeof(int))
                    {
                        sql += setAndGet[0].Invoke(o, null);
                    }
                }

                sql += ",";
            }
        }
        sql = sql.Remove(sql.Length - 1);
        sql += " " + conditions;
        return sql;
    }
    public void executeSQL(string sql, SqlConnection con)
    {
        try
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
        finally
        {
            con.Close();
        }
    }


}



public class Champ
{
    string label;
    string name;
    ArrayList elements;

    public Champ(string label, string name, ArrayList elements)
    {
        this.Label = label;
        this.Name = name;
        this.elements = elements;
    }

    public string Label { get => label; set => label = value; }
    public string Name { get => name; set => name = value; }

    public string gethtml()
    {
        if (this.Equals(new Champ(null, null, null)))
        {
            return "";
        }
        if (String.IsNullOrEmpty(this.Label) || String.IsNullOrEmpty(this.Name))
        {
            string h = "";
            return h;
        }
        string html = "<td><label>" + this.Label + "</label></td>";
        html += "<td>";
        if (elements == null)
        {
            html += "<input type=\"text\" name=\"" + this.Name + "\" >";
        }
        else
        {

            for (int i = 0; i < elements.Count; i++)
            {
                PropertyInfo[] pi = elements[i].GetType().GetProperties();
                MethodInfo GetValue = pi[0].GetGetMethod();
                MethodInfo GetTest = pi[1].GetGetMethod();
                if (elements.Count < 6)
                {
                    html += "<input type=\"checkbox\" name=\"" + this.Name + "\" value=\"" + GetValue.Invoke(elements[i], null) + "\">" + GetTest.Invoke(elements[i], null) + "<br/>";
                }
                else
                {
                    if (i == 0)
                    {
                        html += "<select name=\"" + this.Name + "\" >";
                    }
                    html += "<option value=\"" + GetValue.Invoke(elements[i], null) + "\">" + GetTest.Invoke(elements[i], null) + "</option>";
                    if (i == elements.Count - 1)
                    {
                        html += "</select>";
                    }
                }
            }
        }
        html += "</td>";
        html += "";
        return html;
    }

}
public class FormulaireGen
{
    Champ[] champs;

    public FormulaireGen(Champ[] champs)
    {
        this.champs = champs;
    }

    public string getHtml(string action)
    {
        string html = "<form method=\"get\" action=\"" + action + "\">";
        html += "<table>";
        for (int i = 0; i < this.champs.Length; i++)
        {
            if (champs[i] != null)
            {
                html += "<tr>";
                html += this.champs[i].gethtml();
                html += "</tr>";
            }
        }
        html += "</table>";
        html += "<input type=\"submit\" value=\"OK\">";
        html += "</form>";
        return html;
    }
}
public class createFormFonctions
{
    public FormulaireGen getFormulaireObject(object o, string tablename, SqlConnection c)
    {
        PropertyInfo[] pi = o.GetType().GetProperties();
        Champ[] ch = new Champ[pi.Length];
        object[] columns = (new ReflectsFunctions()).columnNames(c, tablename);
        for (int i = 0; i < pi.Length; i++)
        {
            if ((new ReflectsFunctions()).isPropertyInTable(columns, pi[i].Name))
            {
                if (pi[i].Name.ToUpper().CompareTo("ID" + tablename.ToUpper()) != 0)
                {
                    if (pi[i].Name.ToUpper().Substring(0, 2).CompareTo("ID") == 0)
                    {
                        int length = pi[i].Name.Length;
                        string fk_tableName = pi[i].Name.Substring(2, length - 2);
                        Console.WriteLine(fk_tableName);
                        object fk_obj = Activator.CreateInstance(Type.GetType("UserMng." + fk_tableName));
                        ArrayList al = (new ReflectsFunctions()).select(fk_obj, fk_tableName, c, "");
                        ch[i] = new Champ(fk_tableName, pi[i].Name, al);
                        Console.WriteLine("f");
                    }
                    else
                    {
                        ch[i] = new Champ(pi[i].Name, pi[i].Name, null);

                    }
                }
                else
                {
                    ch[i] = new Champ(null, null, null);
                }
            }
        }
        FormulaireGen form = new FormulaireGen(ch);
        return form;
    }

}
public class CheckDoublon
{
    string constructConditions(object o, string attributs, string tablename)
    {
        string conditions = "";
        char[] seps = new char[] { ';', '/', '-' };
        string[] attr_separes = attributs.Split(seps);
        PropertyInfo[] p_info = o.GetType().GetProperties();
        PropertyInfo[] selected_attributs = new PropertyInfo[attr_separes.Length];
        int i = 0;
        if (attributs.CompareTo("*") == 0)
        {
            selected_attributs = p_info;
        }
        else
        {
            foreach (PropertyInfo p in p_info)
            {
                for (int j = 0; j < attr_separes.Length; j++)
                {
                    if (attr_separes[i].ToUpper().CompareTo(p.Name.ToUpper()) == 0)
                    {
                        selected_attributs[i] = p;
                        i++;
                        break;
                    }
                }
            }
        }
        i = selected_attributs.Length;
        foreach (PropertyInfo p in selected_attributs)
        {
            MethodInfo get = p.GetGetMethod();
            if (get.ReturnType == typeof(int))
            {
                conditions += p.Name + "=" + get.Invoke(o, null) + " and ";
            }
            else
            {
                object g = get.Invoke(o, null);
                if (get.ReturnType == typeof(string) && g != null)
                {
                    conditions += p.Name + "='" + g + "'" + " and ";
                }
                if (get.ReturnType == typeof(DateTime) && g != null)
                {
                    conditions += p.Name + "='" + g + "'" + " and ";

                }
            }

        }
        conditions = conditions.Substring(0, conditions.Length - 4);

        return conditions;
    }
    public bool check(object o, string attributs, string tablename, SqlConnection c)
    {
        bool v = false;
        string conditions = constructConditions(o, attributs, tablename);
        if (String.IsNullOrEmpty(conditions))
        {
            throw new Exception("33");
        }
        else
        {
            string sql = "select * from " + tablename + " where " + conditions;
            Console.WriteLine(sql);
            SqlDataReader rd = (new SqlCommand(sql, c)).ExecuteReader();
            if (rd.HasRows)
            {
                v = true;
            }
            rd.Close();
        }
        return v;
    }

}

public class Formule
{
    public string[] splitFirst(string FormuleBase)
    {
        char[] separateurs = new char[] { '+', '-' };
        string[] res = FormuleBase.Split(separateurs);
        return res;

    }

    public string[] splitSecond(string FormuleBase)
    {
        char[] separateurs = new char[] { '*', 'x', '/' };
        string[] res = FormuleBase.Split(separateurs);
        return res;

    }

    /*public double getValue(string codeBase, ElementCalcule[] EcsrehetraBase)
    {
        double valiny = 2;
        for (int i = 0; i < EcsrehetraBase.Length; i++)
        {
            if (EcsrehetraBase[i].Code.CompareTo(codeBase) == 0)
            {
                string val = EcsrehetraBase[i].Valeur;
                if (val.Contains("["))
                {
                    valiny = calculerFormule(val, EcsrehetraBase);
                }
                else
                {
                    valiny = double.Parse(val);
                }
            }
        }
        return valiny;
    }*/

    public ArrayList getFoisDiv(string Formule)
    {
        ArrayList operations = new ArrayList();
        char[] tab = Formule.ToCharArray();
        operations.Add('*');
        for (int i = 0; i < tab.Length; i++)
        {
            if (tab[i] == '*')
            {
                operations.Add(tab[i]);
            }
            if (tab[i] == '/')
            {
                operations.Add(tab[i]);
            }
        }
        return operations;
    }
    public ArrayList getPlusMoins(string Formule)
    {
        ArrayList operations = new ArrayList();
        char[] tab = Formule.ToCharArray();
        operations.Add('+');
        for (int i = 0; i < tab.Length; i++)
        {
            if (tab[i] == '+')
            {
                operations.Add(tab[i]);
            }
            if (tab[i] == '-')
            {
                operations.Add(tab[i]);
            }
        }
        return operations;
    }

    /*public double calculerFormule(string Formule, ElementCalcule[] ecs)
    {
        string[] splitePlus = splitFirst(Formule);
        ArrayList operationsPlus = getPlusMoins(Formule);
        double rep = 0;
        double[] termes = new double[splitePlus.Length];
        for (int i = 0; i < splitePlus.Length; i++)
        {
            ArrayList operationsFois = getFoisDiv(splitePlus[i]);
            string[] spliteFois = splitSecond(splitePlus[i]);
            double repFois = 1;
            for (int j = 0; j < spliteFois.Length; j++)
            {
                char operationFois = (char)operationsFois[j];
                if (operationFois == '*')
                {
                    repFois = repFois * getValue(spliteFois[j], ecs);
                }
                if (operationFois == '/')
                {
                    repFois = repFois / getValue(spliteFois[j], ecs);
                }
            }
            termes[i] = repFois;

        }
        for (int k = 0; k < termes.Length; k++)
        {
            char operationPlus = (char)operationsPlus[k];
            if (operationPlus == '+')
            {
                rep = rep + termes[k];
            }
            if (operationPlus == '-')
            {
                rep = rep - termes[k];
            }
        }


        return rep;

    }*/




}
