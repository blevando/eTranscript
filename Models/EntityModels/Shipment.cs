namespace eTranscript.Models.EntityModels
{
    public class Shipment
    {
        public int id { get; set; }
        public string? Name { get; set; } // item 

        public decimal Price { get; set; }        

        public int Status { get; set; }

    }
}
 