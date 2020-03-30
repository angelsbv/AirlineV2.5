﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineV2._5.Models
{
    public class Flight
    {
        [Key]
        public int FlgID { get; set; }

        public Aircraft aircraft { get; set; }
        [ForeignKey("aircraft")]
        [Column(TypeName = "int")]
        public int AcID { get; set; }

        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime FlgDeparture { get; set; }

        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime FlgArrival { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int FlgFare { get; set; }

        [Column(TypeName = "varchar(35)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string FlgCategory { get; set; }

    }
}
