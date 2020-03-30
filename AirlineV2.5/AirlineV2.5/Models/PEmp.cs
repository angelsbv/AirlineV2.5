using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineV2._5.Models
{
    public class PEmp { }

    [MetadataType(typeof(PEmp))]
    public partial class Employee
    {
        public string NombreCompleto { get { return $"{EmpName} {EmpLastName}"; } }
        public IEnumerable<FlightAssignedTo> FlightsAssigned { get; set; }
    }
}
