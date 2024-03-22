using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class EntitieValidation : Exception
    {
        public EntitieValidation(string error) : base(error)
        { }

        public static void When(bool hasError, string message)
        {
            if (hasError)
                throw new EntitieValidation(message);
        }
    }
}