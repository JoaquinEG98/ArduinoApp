using ArduinoApp.Models;
using ArduinoApp.Models.DTOs;
using ArduinoApp.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Services
{
    public class LocalService : Conexion.Local.Acceso
    {
        #region Inyección de dependencias
        private readonly Fill _fill;
        private readonly CloudService _cloudService;

        public LocalService()
        {
            _fill = new Fill();
            _cloudService = new CloudService();
        }
        #endregion

        #region Querys
        private const string INSERTAR_HUMEDAD = "INSERT INTO Datos (Fecha, Medicion, Porcentaje, Subido) OUTPUT inserted.Id VALUES (@parFecha, @parMedicion, @parPorcentaje, @parSubido)";
        private const string MARCAR_SUBIDO = "UPDATE Datos SET Subido = 1 OUTPUT inserted.Id WHERE Id = @parId";
        private const string OBTENER_NO_SUBIDOS = "SELECT * FROM Datos WHERE Subido = 0";
        #endregion

        #region Métodos CRUD
        public int InsertarLocal(HumedadDTO humedad)
        {
            try
            {
                ExecuteCommandText = INSERTAR_HUMEDAD;

                ExecuteParameters.Parameters.Clear();


                ExecuteParameters.Parameters.AddWithValue("@parFecha", DateTime.Parse(humedad.Fecha));
                ExecuteParameters.Parameters.AddWithValue("@parMedicion", humedad.Humedad);
                ExecuteParameters.Parameters.AddWithValue("@parPorcentaje", humedad.Porcentaje);
                ExecuteParameters.Parameters.AddWithValue("@parSubido", 0);

                return ExecuteNonEscalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public int MarcarSubido(Humedad humedad)
        {
            try
            {
                ExecuteCommandText = MARCAR_SUBIDO;

                ExecuteParameters.Parameters.Clear();

                ExecuteParameters.Parameters.AddWithValue("@parId", humedad.Id);

                int idSubido = ExecuteNonEscalar();
                if (idSubido > 0)
                {
                    _cloudService.Insertar(humedad);
                }

                return idSubido;
            }
            catch
            {
                throw new Exception("Error en la base de datos.");
            }
        }

        public List<Humedad> ObtenerNoSubidos()
        {
            try
            {
                SelectCommandText = String.Format(OBTENER_NO_SUBIDOS);
                DataSet ds = ExecuteNonReader();

                List<Models.Humedad> humedades = new List<Models.Humedad>();

                if (ds.Tables[0].Rows.Count > 0)
                    humedades = _fill.FillListHumedad(ds);

                return humedades;
            }
            catch (Exception)
            {
                throw new Exception("Error en la base de datos.");
            }
        }
        #endregion
    }
}
