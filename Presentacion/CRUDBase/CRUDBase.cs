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
        }

        private void CRUDBase_Load(object sender, EventArgs e)
        {
            panelAgregar.Visible = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarInformacion();
        }

        private void guardarInformacion() {

            LUsuario lu = new LUsuario();
            DUsuario du = new DUsuario();
            lu.Nombre = txtUser.Text;
            lu.Contrasenia=txtPass.Text;

            //1. Procesa la Imagen


            lu.Estado = "Activo";
            

        }
    }
}
