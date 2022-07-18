using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoApp.Tools
{
    public class Fill
    {
        #region Datos
        public Models.Humedad FillObjectHumedad(DataRow dr)
        {
            Models.Humedad humedad = new Models.Humedad();

            try
            {
                if (dr.Table.Columns.Contains("Id") && !Convert.IsDBNull(dr["Id"]))
                    humedad.Id = Convert.ToInt32(dr["Id"]);

                if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                    humedad.Fecha = Convert.ToDateTime(dr["Fecha"]);

                if (dr.Table.Columns.Contains("Medicion") && !Convert.IsDBNull(dr["Medicion"]))
                    humedad.Medicion = Convert.ToString(dr["Medicion"]);

                if (dr.Table.Columns.Contains("Porcentaje") && !Convert.IsDBNull(dr["Porcentaje"]))
                    humedad.Porcentaje = Convert.ToString(dr["Porcentaje"]);

                if (dr.Table.Columns.Contains("Subido") && !Convert.IsDBNull(dr["Subido"]))
                    humedad.Subido = Convert.ToBoolean(dr["Subido"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método FillObject, " + ex.Message);
            }

            return humedad;
        }

        public List<Models.Humedad> FillListHumedad(DataSet ds)
        {
            return (from DataRow dr in ds.Tables[0].Rows select (new Fill()).FillObjectHumedad(dr)).ToList();
        }
        #endregion
    }
}
