using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopKaraoke.Logica
{
    public class LUsuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Contrasenia { get; set; }

        public byte[] Icon { get; set; }

        public string Estado { get; set; }
    }
}
