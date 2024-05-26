using Domain;

namespace DTO.Out
{
  public class MaintenanceOperatorOutput
  {
    public int Id { get; set; }

    public MaintenanceOperatorOutput(MaintenanceOperator maintenanceOperator)
    {
      Id = maintenanceOperator.Id;
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