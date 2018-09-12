using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Context
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public SocialNetworkContext()
            : base(Properties
                  .Settings
                  .Default.DbConnectionString)
        {
        }
    }
}
