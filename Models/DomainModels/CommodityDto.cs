using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
 

namespace eTranscript.Models.DomainModels
{
    public class CommodityDto
    {       
        public string? Item { get; set; }
        
        public List<ShipmentDto> Shipment { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

            
    }
}
