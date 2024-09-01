namespace eTranscript.Models.DomainModels
{
    public class ShipmentDto
    {

        public int ShipmentId { get; set; }
        public string? Name { get; set; } // item 

        public decimal Price { get; set; }

        public int Status { get; set; } // Checked or not T/F

    }
}
