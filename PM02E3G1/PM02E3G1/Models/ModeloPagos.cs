using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM02E3G1.Models
{
    public class ModeloPagos
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id_Pago { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Photo_recibo { get; set; }
    }
}
