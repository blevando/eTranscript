using System.ComponentModel.DataAnnotations.Schema;

namespace eTranscript.Models.EntityModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } // Order FK

        public string? Item { get; set; } // Commodity FK

        public OrderItemType OrderItemType { get; set; } // This is an enum 
        public string? Address { get; set; } // item 

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
 

    }
public enum OrderItemType
{
    Document,
    Shipment,
    Others

}
 
}
