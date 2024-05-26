using Domain;

namespace DTO.In
{
    public class AdministratorCreateInput
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Administrator ToEntity()
        {
            return new Administrator
            {
                Name = Name,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }
    }
}