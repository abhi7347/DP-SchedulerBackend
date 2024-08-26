using DPScheduler.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.DAL.Interface
{
    public interface IEventOperationsRepo
    {
        public Task CreateEvent(EventDTO EventModel);
        public Task DeleteEvent(string EventModel);
        public Task UpdateEvent(EventDTO EventModel);
        public Task<IEnumerable<dynamic>> GetBookedAppointments(DateTime selectedDate, IEnumerable<int> LocationIds);

    }
}
