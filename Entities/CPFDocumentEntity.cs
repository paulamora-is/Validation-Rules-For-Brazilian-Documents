namespace DocumentValidator.Entities
{
    public class CPFDocumentEntity
    {
        public string? CPFValue { get; set; }
        public string? FirstDigit { get; set; }
        public string? LastDigit { get; set; }

        public static string ReplaceDashAndDot(string cpfValue)
        {
            var cpfValueResult = cpfValue
                .Trim()
                .Replace(" ", "")
                .Replace(".", "")
                .Replace("-", "");
            return cpfValueResult;
        }
    }
}