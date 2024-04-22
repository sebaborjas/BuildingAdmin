using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class PasswordNotFollowPolicy : Exception
    {
        public static string Message = "La contraseña ingresada no cumple con las politicas establecidas.";

        public PasswordNotFollowPolicy() : this(Message) { }

        private PasswordNotFollowPolicy(string message) : base(message) { }

    }
}