using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.DomainModels
{
    public class CommodityDto
    {       
        public string? Item { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }


    }
}
