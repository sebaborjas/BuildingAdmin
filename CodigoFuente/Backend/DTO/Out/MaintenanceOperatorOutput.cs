using Domain;

namespace DTO.Out
{
    public class MaintenanceOperatorOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public MaintenanceOperatorOutput(MaintenanceOperator maintenanceOperator)
        {
            Id = maintenanceOperator.Id;
            Name = maintenanceOperator.Name;
            LastName = maintenanceOperator.LastName;
            Email = maintenanceOperator.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            MaintenanceOperatorOutput maintenanceOperatorModel = (MaintenanceOperatorOutput)obj;
            return Id == maintenanceOperatorModel.Id;
        }
    }
}