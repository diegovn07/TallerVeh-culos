using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Repuesto
    {
        public Object id { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "Unidades Disponibles")]
        public int stock { get; set; }
        [Display(Name = "Precio de Venta")]
        public double precio_venta { get; set; }
    }
}
