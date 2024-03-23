namespace Domain.Validations
{
    public class EntitieValidator : Exception
    {
        public EntitieValidator(string error) : base(error)
        { }

        public static void When(bool hasError, string message)
        {
            if (hasError)
                throw new EntitieValidator(message);
        }
    }
}