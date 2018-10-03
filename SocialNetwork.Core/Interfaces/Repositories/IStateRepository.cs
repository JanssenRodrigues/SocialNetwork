using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces.Repositories
{
    public interface IStateRepository
    {
        State Get(int? id);
        //State GetCountryNameById(int? id);
        IEnumerable<State> GetAll();
        State Create(State state);
        State Edit(State state);
        State Delete(State state);
    }
}
