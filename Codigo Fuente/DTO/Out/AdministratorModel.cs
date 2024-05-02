using Domain;

namespace DTO.Out
{
    public class AdministratorModel
    {

      public AdministratorModel(Administrator administrator)
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
        AdministratorModel administratorModel = (AdministratorModel)obj;
        return Id == administratorModel.Id;
      }
    }
}