using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineV2._5.Models
{
    public class Client
    {
        [Key]
        public int ClnID { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClnName { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClnLastName { get; set; }

        [Column(TypeName = "varchar(35)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClnPhone { get; set; }

        [Column(TypeName = "varchar(320)")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ClnEmail { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime ClnBirthdate { get; set; }

        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime ClnRegisterDate { get; set; }

    }
}
