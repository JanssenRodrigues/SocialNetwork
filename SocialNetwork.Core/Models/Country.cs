using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class Country
    {
        public Country()
        {
            States = new List<State>();
        }

        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
