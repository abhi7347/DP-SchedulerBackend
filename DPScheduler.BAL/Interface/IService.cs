using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.BAL.Interface
{
    public interface IService
    {
        public Task<IEnumerable> GetAllLocations();
        public Task<IEnumerable> GetProvidersByLocations(string dayOfWeek, IEnumerable<int> locationIds);
        public Task<IEnumerable> ProvidersByToggelLocations(string dayOfWeek);

    }
}
