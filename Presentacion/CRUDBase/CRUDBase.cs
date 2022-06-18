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

        private void button1_Click(object sender, EventArgs e)
        {
            panelAgregar.Visible = true;
            btnEditar.Visible = false;
        }

        private void CRUDBase_Load(object sender, EventArgs e)
        {
            panelAgregar.Visible = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUser.Text))
            {


                if (!String.IsNullOrEmpty(txtPass.Text))
                {
                    guardarInformacion();
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
                btnEditar.Visible = true;

            }

        }
    }
}
