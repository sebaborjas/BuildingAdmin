using Domain;

namespace DTO.In
{
    public class CompanyAdministratorCreateInput
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CompanyAdministrator ToEntity()
        {
            return new CompanyAdministrator
            {
                Name = Name,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }
    }
}