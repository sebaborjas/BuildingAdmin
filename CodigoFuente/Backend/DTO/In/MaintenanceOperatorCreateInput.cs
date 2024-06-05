using Domain;

namespace DTO.In
{
    public class MaintenanceOperatorCreateInput
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> Buildings { get; set; } = [];

        public MaintenanceOperator ToEntity()
        {
            var buildings = new List<Building>();
            foreach (var buildingId in Buildings)
            {
                buildings.Add(new Building() { Id = buildingId });
            };
            return new MaintenanceOperator
            {
                Name = Name,
                LastName = LastName,
                Email = Email,
                Password = Password,
                Buildings = buildings
            };
        }
    }
}