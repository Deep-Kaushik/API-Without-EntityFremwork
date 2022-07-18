using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication
{
    public class EmpUnitOfWork
    {
        /* Tradition ADO.Net Approch for connectivity*/
        public int SaveDataStoredProcedure(string StoredProcedureName, string[] ParametersName, params object[] ParametersValues)
        {
            int result = 0;

            //using (var context = new OneAdEntities())
            //{
            //Read the connection string from Web.Config file 
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Employee"]))
            {
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand(StoredProcedureName, con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                //Add the output parameter to the command object 
                int i = 0;
                foreach (string oName in ParametersName)
                {
                    SqlParameter ForeignKeyTablePrm = new SqlParameter(oName, ParametersValues[i]);
                    cmd.Parameters.Add(ForeignKeyTablePrm);
                    i++;
                }

                //Open the connection and execute the query
                con.Open();
                result = cmd.ExecuteNonQuery();

            }
            //}
            return result;

        }



    }
}
