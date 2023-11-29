using SistemaVentas.Entidades;
using SisVenttas.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
    public class FLogin
    {
        public static DataSet ValidarLogin(string sUsuario, string sPassword)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 0, sUsuario),
                    FDBHelper.MakeParam("@Password", SqlDbType.VarChar, 0, sPassword)
                };
            return FDBHelper.ExecuteDataSet("usp_Data_Flogin_ValidarLogin", dbParams);

        }
    }
}
