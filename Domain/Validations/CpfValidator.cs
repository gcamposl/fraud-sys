using System.Text.RegularExpressions;

namespace Domain.Validations
{
    public static class CpfValidator
    {
        public static bool IsValidCpf(string cpf)
        {
            // Verifica se o CPF está no formato correto
            bool isValidFormat = Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

            if (!isValidFormat)
                return false;

            // Extrai os dígitos numéricos do CPF
            string digits = cpf.Replace(".", "").Replace("-", "");

            // Calcula os dígitos verificadores
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(digits[i].ToString()) * (10 - i);
            int firstDigit = 11 - (sum % 11);
            if (firstDigit >= 10)
                firstDigit = 0;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(digits[i].ToString()) * (11 - i);
            int secondDigit = 11 - (sum % 11);
            if (secondDigit >= 10)
                secondDigit = 0;

            // Verifica se os dígitos verificadores calculados são iguais aos informados no CPF
            return digits[9] == firstDigit.ToString()[0] && digits[10] == secondDigit.ToString()[0];
        }
    }
}