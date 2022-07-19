using ArduinoApp.Conexion;
using ArduinoApp.Models;
using ArduinoApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Services
{
    public class CloudService : Conexion.Cloud.Acceso
    {
        #region Querys
        private const string INSERTAR_HUMEDAD = "INSERT INTO Datos (Fecha, Medicion, Porcentaje) OUTPUT inserted.Id VALUES (@parFecha, @parMedicion, @parPorcentaje)";
        #endregion

        #region Métodos CRUD
        public int Insertar(Humedad humedad)
        {
            try
            {
                ExecuteCommandText = INSERTAR_HUMEDAD;

                ExecuteParameters.Parameters.Clear();


                ExecuteParameters.Parameters.AddWithValue("@parFecha", humedad.Fecha);
                ExecuteParameters.Parameters.AddWithValue("@parMedicion", humedad.Medicion);
                ExecuteParameters.Parameters.AddWithValue("@parPorcentaje", humedad.Porcentaje);

                return ExecuteNonEscalar();
            }
            catch
            {
                 throw new Exception("Error en la base de datos.");
            }
        }
        #endregion
    }
}
