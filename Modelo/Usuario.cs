using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        
        public Object id { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        public string PApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SApellido { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }
    }
}
