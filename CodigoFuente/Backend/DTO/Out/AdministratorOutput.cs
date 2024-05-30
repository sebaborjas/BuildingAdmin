using Domain;

namespace DTO.Out
{
    public class AdministratorOutput
    {

      public AdministratorOutput(Administrator administrator)
      {
        Id = administrator.Id;
      }
      public int Id { get; set; }

      public override bool Equals(object obj)
      {
        if (obj == null || GetType() != obj.GetType())
        {
          return false;
        }
        AdministratorOutput administratorModel = (AdministratorOutput)obj;
        return Id == administratorModel.Id;
      }
    }
}