using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DesktopKaraoke.Datos
{
    public class ConexionMaestra
    {
        private string conexionMaestra;
        public SqlConnection conexion;
        public ConexionMaestra()
        {
            conexionMaestra = @"Data source=NEIL\BDS3;
                Initial Catalog=DB369Basico;
                Integrated Security=true";

            conexion=new SqlConnection(conexionMaestra);
        }

        public void AbrirConexion() {
            if (conexion.State==ConnectionState.Closed) {
                conexion.Open();
            }
        }

        public void CerraConexion()
        {
            if (conexion.State==ConnectionState.Open) {
                conexion.Close();
            }
        }
    }
}
