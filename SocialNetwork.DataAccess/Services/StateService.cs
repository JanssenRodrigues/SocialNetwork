using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Services
{
    public class StateService
    {
        private IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public State CreateState(State state)
        {
            return _stateRepository.Create(state);
        }

        public State EditState(State state)
        {
            return _stateRepository.Edit(state);
        }

        public State DeleteState(State state)
        {
            return _stateRepository.Delete(state);
        }

        public State GetState(int? id)
        {
            return _stateRepository.Get(id);
        }

        public IEnumerable<State> GetAllCountries()
        {
            return _stateRepository.GetAll();
        }
    }
}
