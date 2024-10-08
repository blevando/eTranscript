﻿using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.DomainModels
{
    public class ShipmentDto
    {
                
        public string? Name { get; set; } // item 

        public string? Address { get; set; } // item 

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
 

    }
}
