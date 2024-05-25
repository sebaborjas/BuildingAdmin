using Domain.DataTypes;

namespace Domain
{
    public class MaintenanceOperator : User
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

        public List<Building> Buildings { get; set; }
    }
}