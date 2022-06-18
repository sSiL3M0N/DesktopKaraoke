using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DesktopKaraoke.Logica;
using System.Windows.Forms;

namespace DesktopKaraoke.Datos
{
    public class DUsuario
    {
        public ConexionMaestra cMaestra;


        public DUsuario()
        {
            cMaestra = new ConexionMaestra();

        }

        public bool AgregarUsuario(LUsuario user) {
            
            try
            {
                cMaestra.AbrirConexion();
                SqlCommand cmd = new SqlCommand("InsertUser", cMaestra.conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", user.Nombre);
                cmd.Parameters.AddWithValue("@Pass", user.Contrasenia);
                cmd.Parameters.AddWithValue("@Icon", user.Icon);
                cmd.Parameters.AddWithValue("@State", user.Estado);

                return  cmd.ExecuteNonQuery()!=0 ? true: false;


            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message);
                return false;
            
            }
            finally {
                cMaestra.CerraConexion();
            }
            
        }
    }
}
