namespace eTranscript.Models.EntityModels
{
    public class PaymentGatewayConfig
    {
        public int Id { get; set; }
        public string PaymentGatewayId { get; set; }
        public string Apikey { get; set; }
        public string Apisecret { get; set; }
        public string GatewayUrl { get; set; }
        public string PaymentHookUrl { get; set; }
        public string IntegrationKey { get; set; }
        public string ReferencePrefix { get; set; }
    }
}
