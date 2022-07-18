using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Models
{
    public class Humedad
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Medicion { get; set; }
        public string Porcentaje { get; set; }
        public bool Subido { get; set; }
    }
}
