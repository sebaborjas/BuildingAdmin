using Domain.DataTypes;

namespace Domain
{
    public class CompanyAdministrator : User
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

        public ConstructionCompany ConstructionCompany { get; set; }
    }
}