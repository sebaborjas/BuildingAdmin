using Domain.DataTypes;

namespace Domain
{
    public class CompanyAdministrator : User
    {
        public ConstructionCompany ConstructionCompany { get; set; }
    }
}