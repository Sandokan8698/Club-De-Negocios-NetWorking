using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Layer.Entities
{
    public class Anticipo
    {
        public int AnticipoId { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal Valor { get; set; }


        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
    }   

}
