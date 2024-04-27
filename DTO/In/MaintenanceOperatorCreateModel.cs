using Domain;

namespace DTO.In
{
    public class MaintenanceOperatorCreateModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Building Building { get; set; }

        public MaintenanceOperator ToEntity()
        {
            return new MaintenanceOperator
            {
                Name = Name,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Building = Building
            };
        }
    }
}