namespace Domain.DTOs
{
    public class TransactionDTO
    {
        public string Source { get; set; }
        public string Destiny { get; set; }
        public decimal Value { get; set; }
    }
}