using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class WrongEmailFormatException : Exception
    {
        public static string Message = "El email ingresado no es correcto";

        public WrongEmailFormatException() : this(Message)
        {

        }

        private WrongEmailFormatException(string? message) : base(message)
        {

        }

    }
}