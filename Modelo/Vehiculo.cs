using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Vehiculo
    {
        [Display(Name = "Placa")]
        public string placa { get; set; }
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        [Display(Name = "Año")]
        public int ano { get; set; }

        public List<Cobros> Cobros
        {
            get; set;
        }
    }
}
