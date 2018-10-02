using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class Profile
    {
        public Profile()
        {
            Galleries = new List<Gallery>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string AccountId { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
    }
}
