namespace eTranscript.Common.Utilities
{
    public static class CommonUtility
    {
        public static string GetOrderNumberByCustomerId(string customerId)
        {
            Guid guid = Guid.NewGuid();

            char[] ch = guid.ToString().ToCharArray();
            string output = string.Concat(ch.Where(Char.IsDigit));

            if (output.Length < 10)
            {
                output += string.Concat(ch.Where(Char.IsDigit));
            }
            output = output.Substring(0, 10);

            return output;

        }

        public static string GetInvoiceNumber()
        {
            throw new NotImplementedException();
        }
    }
}
