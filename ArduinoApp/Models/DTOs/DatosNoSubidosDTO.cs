using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Models.DTOs
{
    public class DatosNoSubidosDTO
    {
        public int Id { get; set; }
        public string Humedad { get; set; }
        public string Porcentaje { get; set; }
        public string Fecha { get; set; }

        public static DatosNoSubidosDTO FillObject(Humedad humedad)
        {
            return new DatosNoSubidosDTO()
            {
                Id = humedad.Id,
                Humedad = humedad.Medicion,
                Porcentaje = humedad.Porcentaje,
                Fecha = humedad.Fecha.ToString("dd/MM/yyyy hh:mm tt"),
            };
        }

        public static List<DatosNoSubidosDTO> FillListDTO(List<Humedad> humedades)
        {
            List<DatosNoSubidosDTO> datosNoSubidosDTO = new List<DatosNoSubidosDTO>();

            if (humedades != null)
            {
                foreach (Humedad humedad in humedades)
                {
                    datosNoSubidosDTO.Add(FillObject(humedad));
                }
            }

            return datosNoSubidosDTO;
        }
    }
}
