using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.EntityModels
{
    public class Commodity
    {
        public int Id { get; set; }

        public string? CategoryId { get; set; }
        public string? Item { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
      

    }
}
