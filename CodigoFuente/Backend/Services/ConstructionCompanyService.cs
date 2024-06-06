using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using IDataAccess;
using Domain;

namespace Services
{
    public class ConstructionCompanyService : IConstructionCompanyService
    {
        public IGenericRepository<ConstructionCompany> _constructionCompanyRepository;

        private ISessionService _sessionService;

        public ConstructionCompanyService(IGenericRepository<ConstructionCompany> constructionCompanyRepository, ISessionService sessionService)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
            _sessionService = sessionService;
        }

        public ConstructionCompany CreateConstructionCompany(string name)
        {
            var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user is not authorized to create a construction company");
            }
            if (currentUser.ConstructionCompany != null)
            {
                throw new InvalidOperationException("Current user already has a construction company");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "Invalid construction company");
            }
            if (_constructionCompanyRepository.GetByCondition(c => c.Name == name) != null)
            {
                throw new ArgumentException("Construction company already exist");
            }
            try
            {
                ConstructionCompany constructionCompany = new ConstructionCompany
                {
                    Name = name
                };
                _constructionCompanyRepository.Insert(constructionCompany);
                return constructionCompany;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error creating construction company", e);
            }
        }

        public ConstructionCompany ModifyConstructionCompany(string name)
        {
            var currentUser = _sessionService.GetCurrentUser() as CompanyAdministrator;
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user is not authorized to update a construction company");
            }
            var constructionCompany = currentUser.ConstructionCompany;
            if (constructionCompany == null)
            {
                throw new InvalidOperationException("Current user does not have a construction company");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "Invalid construction company");
            }
            if (_constructionCompanyRepository.GetByCondition(c => c.Name == name) != null)
            {
                throw new ArgumentException("Construction company already exist");
            }
            try
            {
                constructionCompany.Name = name;
                _constructionCompanyRepository.Update(constructionCompany);
                return constructionCompany;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error updating construction company", e);
            }
        }

        public ConstructionCompany GetUserCompany()
        {
            return null;
        }
    }
}
