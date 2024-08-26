using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.DAL.Interface
{
    public interface IRepository
    {
        public Task<IEnumerable> GetAllLocations();
        public Task<IEnumerable<dynamic>> ProviderByLocations(string dayOfWeek, IEnumerable<int> locationIds);
        public Task<IEnumerable<dynamic>> ProvidersByToggelLocations(string dayOfWeek);
    }
}
                