using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.EntityModels
{
    public class Shipment
    {
        public int Id { get; set; }
        public string? Name { get; set; } // item 

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }        
        
        public int Status { get; set; }

    }
}
 