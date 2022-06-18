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

        public DataTable BuscarUsuario(String parame)
        {
            try
            {
                cMaestra.AbrirConexion();

                SqlCommand sqlCommand = new SqlCommand("buscarUser", cMaestra.conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@param", parame);

                if (sqlCommand.ExecuteNonQuery() != 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da=new SqlDataAdapter(sqlCommand);
                    da.Fill(dt);
                    return dt;

                }
                else
                {
                    return null;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return null;

            }
            finally
            {
                cMaestra.CerraConexion();

            }
        }

        public bool EliminarUsuario(int eliminarUsuario) {
            try
            {
                cMaestra.AbrirConexion();
                SqlCommand sqlCommand = new SqlCommand("eliminarUser", cMaestra.conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", eliminarUsuario);

                return sqlCommand.ExecuteNonQuery() != 0 ? true : false;


            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return false;

            }
            finally
            {
                cMaestra.CerraConexion();

            }
        }

        public bool EditarUsuario(LUsuario user) {
            try
            {
                cMaestra.AbrirConexion();
                SqlCommand sqlCommand = new SqlCommand("editarUser", cMaestra.conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id",user.IdUsuario);
                sqlCommand.Parameters.AddWithValue("@name", user.Nombre);
                sqlCommand.Parameters.AddWithValue("@pass", user.Contrasenia);
                sqlCommand.Parameters.AddWithValue("@icon", user.Icon);
                sqlCommand.Parameters.AddWithValue("@state", user.Estado);

                return sqlCommand.ExecuteNonQuery() != 0 ? true : false;

            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return false;
            }
            finally {
                cMaestra.CerraConexion();
            }
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

        public DataTable listarUsuarios() {
            try
            {
                cMaestra.AbrirConexion();
                SqlCommand cmd = new SqlCommand("listarUser",cMaestra.conexion);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqla = new SqlDataAdapter(cmd);
                    sqla.Fill(dt);
                    return dt;
                }
                else {
                    return null;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show (e.Message);
                return null;
            }
            finally {
                cMaestra.CerraConexion();
            }
        }
    }
}
