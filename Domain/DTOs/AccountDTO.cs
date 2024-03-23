namespace Domain.DTOs
{
    public class AccountDTO
    {
        public string Cpf { get; set; }
        public int Agency { get; set; }
        public int AccountNumber { get; set; }
        public decimal Limit { get; set; }
    }
}