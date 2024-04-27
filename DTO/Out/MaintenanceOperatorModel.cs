using Domain;

namespace DTO.Out
{
  public class MaintenanceOperatorModel
  {
    public int Id { get; set; }

    public MaintenanceOperatorModel(MaintenanceOperator maintenanceOperator)
    {
      Id = maintenanceOperator.Id;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }
      MaintenanceOperatorModel maintenanceOperatorModel = (MaintenanceOperatorModel)obj;
      return Id == maintenanceOperatorModel.Id;
    }
  }
}