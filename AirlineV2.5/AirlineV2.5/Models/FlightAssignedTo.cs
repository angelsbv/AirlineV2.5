using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineV2._5.Models
{
    public class FlightAssignedTo
    {
        [Key]
        public int FlgEmpID { get; set; }

        public Flight flight { get; set; }
        [ForeignKey("flight")]
        [Column(TypeName = "int")]
        public int FlgID { get; set; }

        public Employee employee { get; set; }
        [ForeignKey("employee")]
        [Column(TypeName = "int")]
        public int EmpID { get; set; }
    }
}
