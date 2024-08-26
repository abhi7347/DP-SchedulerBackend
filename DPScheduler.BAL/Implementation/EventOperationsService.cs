using DPScheduler.BAL.Interface;
using DPScheduler.DAL.DTOs;
using DPScheduler.DAL.Implementation;
using DPScheduler.DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.BAL.Implementation
{
    public class EventOperationsService: IEventOperationsService
    {
        private readonly IEventOperationsRepo _eventOpsRepo;

        public EventOperationsService(IEventOperationsRepo eventOps)
        {
            _eventOpsRepo = eventOps;
        }

        public async Task CreateEvent(EventDTO EventModel)
        {
            await _eventOpsRepo.CreateEvent(EventModel);
        }


        // Delete Event Method
        public async Task DeleteEvent(string EventModel)
        {
           await _eventOpsRepo.DeleteEvent(EventModel);
        }

        //Update Event Method
        public async Task UpdateEvent(EventDTO EventModel)
        {
            await _eventOpsRepo.UpdateEvent(EventModel);
        }

        // Get Booked Appointments
        public async Task<IEnumerable> GetBookedAppointments(DateTime selectedDate, IEnumerable<int> LocationIds)
        {
            return await _eventOpsRepo.GetBookedAppointments(selectedDate, LocationIds);
        }
    }
}
