namespace Domain
{
    public class Administrator : User
    {

        public override string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
                _lastName = value;
            }
        }
    }
}