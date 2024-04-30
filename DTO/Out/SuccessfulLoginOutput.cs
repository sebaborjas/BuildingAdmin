using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class SuccessfulLoginOutput
    {
        public Guid Token { get; set; }

        public SuccessfulLoginOutput(Session session)
        {
            this.Token = session.Token;
        }
    }

    
}
