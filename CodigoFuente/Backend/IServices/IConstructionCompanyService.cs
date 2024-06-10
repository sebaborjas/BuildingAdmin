using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IConstructionCompanyService
    {
        public ConstructionCompany CreateConstructionCompany(string name);

        public ConstructionCompany ModifyConstructionCompany(string name);

        public ConstructionCompany GetUserCompany();
    }   
}
