using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cliente
    {
        public Object id { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Primer Apellido")]
        public string PApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string SApellido { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Display(Name = "Reparaciones")]
        public List<Vehiculo> Reparaciones
        {
            get; set;
        }
    }
}
