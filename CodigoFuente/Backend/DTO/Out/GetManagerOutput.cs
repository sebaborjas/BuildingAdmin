using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Out
{
    public class GetManagerOutput
    {
        public GetManagerOutput(Manager manager)
        {
            Id = manager.Id;
            Name = manager.Name;
            Email = manager.Email;
            Buildings = manager.Buildings.Select(b => b.Id).ToList();
        }

        public GetManagerOutput() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<int> Buildings { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GetManagerOutput)
            {
                var manager = obj as GetManagerOutput;
                if (manager.Id == Id && manager.Name == Name && manager.Email == Email)
                {
                    foreach(var building in Buildings)
                    {
                        if (!manager.Buildings.Contains(building))
                        {
                            return false;
                        }
                    }
                    return true;
                };
            }
            return false;
        }
    }
}
