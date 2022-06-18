using DesktopKaraoke.Datos;
using DesktopKaraoke.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DesktopKaraoke.Presentacion.CRUDBase
{
    public partial class CRUDBase : Form
    {
        private int idUsuario;
        public CRUDBase()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Foto de perfil";
            openFileDialog.Filter = "Imagnes PNG|*.png|Imagenes JPG|*.jpg";
            openFileDialog.InitialDirectory = "";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog()==DialogResult.OK) {

                pbIcon.BackgroundImage = null;
                pbIcon.Image = new Bitmap(openFileDialog.FileName);

            }
        }

     
        private void CRUDBase_Load(object sender, EventArgs e)
        {
            listarUsuario();
            panelAgregar.Visible = false;

        }

        private void listarUsuario() {
            dataUsuarios.Dock = DockStyle.Fill;
            DUsuario usuario = new DUsuario();
            DataTable dt = new DataTable();
            dt = usuario.listarUsuarios();
            dataUsuarios.DataSource = dt;


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUser.Text))
            {


                if (!String.IsNullOrEmpty(txtPass.Text))
                {
                    guardarInformacion();
                    listarUsuario();
                }
                else {
                    MessageBox.Show("Error", "Verifica la contraseña", MessageBoxButtons.OK);
                }

            }
            else {
                MessageBox.Show("Error", "Verifica el usuario", MessageBoxButtons.OK);
            }

            
        }

        private void guardarInformacion() {

            LUsuario lu = new LUsuario();
            DUsuario du = new DUsuario();

            lu.Nombre = txtUser.Text;
            lu.Contrasenia=txtPass.Text;

            //1. Procesa la Imagen
            //2. Interpreta la imagen
            //3. Se agrega imagen a SQL Server
            System.IO.MemoryStream ms =new System.IO.MemoryStream();


            pbIcon.Image.Save(ms, pbIcon.Image.RawFormat);

            lu.Icon = ms.GetBuffer();

            lu.Estado = "Activo";

            if (du.AgregarUsuario(lu)) {
                MessageBox.Show("Agregado","Nuevo usuario añadido");
                panelAgregar.Visible=false;
                panelAgregar.Dock = DockStyle.None;

            }

        }

        private void btnAgregar_click(object sender, EventArgs e)
        {
            panelAgregar.Visible = true;
            panelAgregar.Dock = DockStyle.Fill;

            btnEditar.Visible = false;
        }

        private void dataUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idUsuario = Convert.ToInt32(dataUsuarios.SelectedCells[2].Value.ToString());

            if (e.ColumnIndex == this.dataUsuarios.Columns["Editar"].Index) {
                txtUser.Text = dataUsuarios.SelectedCells[3].Value.ToString();
                txtPass.Text = dataUsuarios.SelectedCells[4].Value.ToString();
                //1. Recibe el valor de la imagen en un array de tipo byte
                byte[] imagen = (Byte[])dataUsuarios.SelectedCells[5].Value;

                //2. Instancia un objeto de tipo MemoryStream con parametro el objeto anterior
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagen);

                //3. Carga la imagen luego del tratamiento 
                pbIcon.Image=Image.FromStream(ms);


                panelAgregar.Visible = true;
                btnGuardar.Visible = false;


            } 

            if(e.ColumnIndex==this.dataUsuarios.Columns["Eliminar"].Index){

                DialogResult dr=MessageBox.Show("Esta opción es irreversible", "¿Estás seguro?",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                if (dr==DialogResult.OK) {
                    EliminarUsuario();
                    listarUsuario();
                }
                
            }
        }
        private void EliminarUsuario() {

            DUsuario dUsuario = new DUsuario();
           
            if (dUsuario.EliminarUsuario(idUsuario)) {
                MessageBox.Show("Usuario eliminado", "El usuario se elimino correctamnte", MessageBoxButtons.OK);
                panelAgregar.Visible=false;
                panelAgregar.Dock= DockStyle.None;  
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            panelAgregar.Visible=false;
            panelAgregar.Dock=DockStyle.None;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editarUsuario();
            listarUsuario();

        }

        private void editarUsuario()
        {

            LUsuario lu = new LUsuario();
            DUsuario du = new DUsuario();
            lu.IdUsuario = idUsuario;
            lu.Nombre = txtUser.Text;
            lu.Contrasenia = txtPass.Text;

            //1. Procesa la Imagen
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            //2. Interpreta la imagen
            pbIcon.Image.Save(ms, pbIcon.Image.RawFormat);

            //3. Se agrega imagen a SQL Server y se recupera
            lu.Icon = ms.GetBuffer();

            lu.Estado = "Activo";

            if (du.EditarUsuario(lu))
            {
                MessageBox.Show("Editado", "Nuevo usuario editado");
                panelAgregar.Visible = false;
                panelAgregar.Dock = DockStyle.None;

            }
            else
            {
                MessageBox.Show("Error", "Erroe");

            }
        }

        private void tbBuscar_TextChanged(object sender, EventArgs e)
        {
            buscarUserPass();
        }

        private void buscarUserPass()
        {
            DUsuario du = new DUsuario();
            DataTable dt;

            dt=du.BuscarUsuario(tbBuscar.Text);

            dataUsuarios.DataSource=dt;

        }
    }
}
