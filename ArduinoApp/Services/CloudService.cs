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
        private const string MARCAR_SUBIDO = "UPDATE Datos SET Subido = 1 OUTPUT inserted.Id WHERE Id = @parId";
        #endregion

        #region Métodos CRUD
        public int Insertar(HumedadDTO humedad)
        {
            try
            {
                ExecuteCommandText = INSERTAR_HUMEDAD;

                ExecuteParameters.Parameters.Clear();


                ExecuteParameters.Parameters.AddWithValue("@parFecha", DateTime.Parse(humedad.Fecha));
                ExecuteParameters.Parameters.AddWithValue("@parMedicion", humedad.Humedad);
                ExecuteParameters.Parameters.AddWithValue("@parPorcentaje", humedad.Porcentaje);

                return ExecuteNonEscalar();
            }
            catch (Exception ex)
            {
                 throw new Exception("Error en la base de datos.");
            }
        }

        public int MarcarSubido(int id)
        {
            try
            {
                ExecuteCommandText = MARCAR_SUBIDO;

                ExecuteParameters.Parameters.Clear();


                ExecuteParameters.Parameters.AddWithValue("@parId", id);

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
