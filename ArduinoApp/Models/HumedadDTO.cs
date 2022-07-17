using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Models
{
    public class HumedadDTO
    {
        public int Humedad { get; set; }
        public string Fecha { get; set; }

        public static HumedadDTO FillObject(string humedad)
        {
            return new HumedadDTO()
            {
                Humedad = int.Parse(humedad),
                Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt")
            };
        }
    }
}
