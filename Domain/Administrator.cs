namespace Domain
{
    public class Administrator : User
    {
        private string _lastName;

        private ICollection<Invitation> _invitations = new List<Invitation>();

        public override string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
                _lastName = value;
            }
        }

        public ICollection<Invitation> Invitations
        {
            get => _invitations;

            set
            {
                if (value == null) throw new ArgumentNullException();
                _invitations = value;
            }
        }
    }
}