using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineV2._5.Models
{
    public class ClnRelFlg
    {
        [Key]
        public int ClnFlgID { get; set; }

        #pragma warning disable IDE1006 // Naming Styles
        public Client client { get; set; }
        [ForeignKey("client")]
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int ClnID { get; set; }

        public Flight flight { get; set; }
        [ForeignKey("flight")]
        [Column(TypeName = "int")]
        public int FlgID { get; set; }

    }
}
