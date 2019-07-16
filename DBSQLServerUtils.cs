using System.Data.SqlClient;



public class DBSQLServerUtils
{

    public static SqlConnection GetDBConnection()
    {
        //
        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\GUP\GUP\App_Data\GUPDB.mdf;Integrated Security=True
        // 
        //
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\RallyWeb\RallyWeb\App_Data\rallyweb.mdf;Integrated Security=True";

        SqlConnection conn = new SqlConnection(connString);

        return conn;
    }


}
