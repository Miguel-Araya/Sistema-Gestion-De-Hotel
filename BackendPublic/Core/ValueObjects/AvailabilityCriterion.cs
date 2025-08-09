using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class AvailabilityCriterion
    {
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int RoomTypeId { get; set;  }


        public AvailabilityCriterion(DateTime entryDate, DateTime departureDate, int roomTypeId)
        {
            // Regla de negocio entrada y salida a medio día.
            EntryDate = SetTimeToNoon(entryDate);
            DepartureDate = SetTimeToNoon(departureDate);

            //validar cuando la fecha de salida es menor a la de entrada
            if (DepartureDate <= EntryDate)
            {
                throw new ArgumentException("La fecha de salida debe ser posterior a la fecha de entrada");
            }

            RoomTypeId = roomTypeId;
        }

        // Método para setear las horas a medio día
        private static DateTime SetTimeToNoon(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
        }
    }

}
