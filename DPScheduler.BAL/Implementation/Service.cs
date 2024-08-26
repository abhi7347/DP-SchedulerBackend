using DPScheduler.BAL.Interface;
using DPScheduler.DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.BAL.Implementation
{
    public class Service: IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable> GetAllLocations()
        {
            return await _repository.GetAllLocations();
        }

        public async Task<IEnumerable> GetProvidersByLocations(string dayOfWeek, IEnumerable<int> locationIds)
        {
            return await _repository.ProviderByLocations(dayOfWeek, locationIds);
        }

        public async Task<IEnumerable> ProvidersByToggelLocations(string dayOfWeek)
        {
            return await _repository.ProvidersByToggelLocations(dayOfWeek);
        }

    }
}
