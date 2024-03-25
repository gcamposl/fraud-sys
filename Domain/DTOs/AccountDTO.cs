using System.Text.Json.Serialization;

namespace Domain.DTOs
{
    public class AccountDTO
    {
        [JsonPropertyName("Cpf")]
        public string Cpf { get; set; }
        [JsonPropertyName("Agency")]
        public int Agency { get; set; }
        [JsonPropertyName("AccountNumber")]
        public int AccountNumber { get; set; }
        [JsonPropertyName("Limit")]
        public decimal Limit { get; set; }
    }
}