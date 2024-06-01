using Domain;

namespace DTO.Out
{
    public class CompanyAdministratorOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ConstructionCompanyOutput ConstructionCompany { get; set; }

        public CompanyAdministratorOutput(CompanyAdministrator companyAdministrator)
        {
            Id = companyAdministrator.Id;
            Name = companyAdministrator.Name;
            LastName = companyAdministrator.LastName;
            Email = companyAdministrator.Email;
            ConstructionCompany = new ConstructionCompanyOutput(companyAdministrator.ConstructionCompany);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            CompanyAdministratorOutput companyAdministratorModel = (CompanyAdministratorOutput)obj;
            return Id == companyAdministratorModel.Id;
        }
    }
}