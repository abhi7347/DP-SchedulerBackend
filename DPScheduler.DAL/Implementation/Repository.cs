using Dapper;
using DPScheduler.DAL.DTOs;
using DPScheduler.DAL.Interface;
using DPScheduler.DAL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.DAL.Implementation
{
    public class Repository:IRepository
    {
        private readonly SchedulerDBContext _context;


        public Repository(SchedulerDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable> GetAllLocations()
        {
            var query = "SELECT * FROM DP_Locations";

            using (var connection = _context.CreateConnection())
            {
                // Use QueryAsync<dynamic> to return dynamic objects
                var locations = await connection.QueryAsync(query);
                return locations;
            }
        }


        // Get Providers Based on Locations
        public async Task<IEnumerable<dynamic>> ProviderByLocations(string dayOfWeek, IEnumerable<int> locationIds)
        {
            var query = "USP_DayPilot_Procedure";

            var parameters = new DynamicParameters();
            parameters.Add("@DayOfWeek", dayOfWeek);
            parameters.Add("@LocationIds", string.Join(",", locationIds));


            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Get Providers for Toggel Locations
        public async Task<IEnumerable<dynamic>> ProvidersByToggelLocations(string dayOfWeek)
        {
            // Define the SQL query
            string sql = @" SELECT  p.FirstName, p.LastName, pl.ProviderId, pl.LocationId, l.LocationName FROM DP_Providers p
                            INNER JOIN 
                                DP_Provider_Locations pl ON p.ProviderId = pl.ProviderId
                            INNER JOIN 
                                DP_Availability a ON p.ProviderId = a.ProviderId
                            INNER JOIN 
                                DP_Locations l ON p.ProviderId = l.LocationId
                            WHERE 
                               a.DayOfWeek = @DayOfWeek; ";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new { DayOfWeek = dayOfWeek };
                    return await connection.QueryAsync<dynamic>(sql, parameters);
                }
            }
            catch (Exception ex) { throw; }
            
                
        }

        

    }
}
