using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Delete
{
    public class Pause
    {
        private readonly IDeletePauseRepository _deletePauseRepository;
        public Pause(IDeletePauseRepository deletePauseRepository)
        {
            _deletePauseRepository = deletePauseRepository;
        }
        public void Delete(string pauseName)
        {
            _deletePauseRepository.DeletPause(pauseName);
        }
    }
}
