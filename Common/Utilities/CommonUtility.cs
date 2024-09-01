namespace eTranscript.Common.Utilities
{
    public static class CommonUtility
    {
        public static string GetOrderNumberByCustomerId(string customerId)
        { 
            
            long ticks = DateTime.UtcNow.Ticks;
            long orderNumber = 0;
            try
            {
                // int customerId = 410024;               

                long orderId = long.Parse(customerId) + ticks;

                orderNumber = orderId.GetHashCode();

                
            }
            catch (Exception ex)
            {
                // There was an issue with using the CustomerId that was passed, therefore improvise
                long improvisedOrderId = long.Parse(DateTime.Now.ToString("yyyyMM")) + ticks;

                orderNumber = improvisedOrderId.GetHashCode();
            }

            return orderNumber.ToString();


        }
    }
}
