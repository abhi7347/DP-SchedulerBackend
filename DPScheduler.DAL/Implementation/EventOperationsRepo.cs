using Dapper;
using DPScheduler.DAL.DTOs;
using DPScheduler.DAL.Interface;
using DPScheduler.DAL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DPScheduler.DAL.Implementation
{
    public class EventOperationsRepo:IEventOperationsRepo
    {
        private readonly SchedulerDBContext _context;

        public EventOperationsRepo(SchedulerDBContext context)
        {
            _context = context;
        }
        public async Task CreateEvent(EventDTO EventModel)
        {
            try
            {
                const string sql = @"
            INSERT INTO DP_Appointments (ProviderId, EventId, EventName, EventDate, StartTime, EndTime, Color, CreatedAt, ModifiedAt)
            VALUES (@ProviderId, @EventId, @EventName, @EventDate, @StartTime, @EndTime, @Color, @CreatedAt, @ModifiedAt)";


                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        ProviderId = EventModel.ProviderId,
                        EventId = EventModel.EventId,
                        EventName = EventModel.EventName,
                        EventDate = EventModel.EventDate,
                        StartTime = EventModel.StartTime,
                        EndTime = EventModel.EndTime,
                        Color = EventModel.BarColor,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    });

                }
            }

            catch (Exception ex)
            {
                throw;
            }
            
        }

        // Delete Event Method
        public async Task DeleteEvent(string eventId)
        {
            const string checkSql = "SELECT COUNT(1) FROM DP_Appointments WHERE EventId = @EventId";
            const string deleteSql = "DELETE FROM DP_Appointments WHERE EventId = @EventId";

            using (var connection = _context.CreateConnection())
            {
                // Check if the record exists
                var exists = await connection.ExecuteScalarAsync<bool>(checkSql, new { EventId = eventId });

                if (!exists)
                {
                    // Record does not exist, throw an exception or handle as needed
                    throw new InvalidOperationException($"Event with ID {eventId} does not exist.");
                }
                else
                {
                    // Delete the record if it exists
                    await connection.ExecuteAsync(deleteSql, new { EventId = eventId });
                }
                
            }
        }

        //Update Event Method
        public async Task UpdateEvent(EventDTO eventModel)
        {
            const string checkSql = "SELECT COUNT(1) FROM DP_Appointments WHERE EventId = @EventId";

            const string sql = @"
            UPDATE DP_Appointments
            SET ProviderId = @ProviderId,
                EventName = @EventName,
                EventDate = @EventDate,
                StartTime = @StartTime,
                EndTime = @EndTime,
                Color = @Color,
                ModifiedAt = @ModifiedAt
                WHERE EventId = @EventId";

            using (var connection = _context.CreateConnection())
            {

                // Check if the record exists
                var exists = await connection.ExecuteScalarAsync<bool>(checkSql, new { EventId = eventModel.EventId });

                if (!exists)
                {
                    // Record does not exist, throw an exception or handle as needed
                    throw new InvalidOperationException($"Event with ID {eventModel.EventId} does not exist.");
                }
                else
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        ProviderId = eventModel.ProviderId,
                        EventId = eventModel.EventId,
                        EventName = eventModel.EventName,
                        EventDate = eventModel.EventDate,
                        StartTime = eventModel.StartTime,
                        EndTime = eventModel.EndTime,
                        Color = eventModel.BarColor,
                        ModifiedAt = DateTime.Now
                    });
                }
                
            }
        }


    }
}
