using Dapper;
using DPScheduler.DAL.DTOs;
using DPScheduler.DAL.Interface;
using DPScheduler.DAL.Model;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;


        public Repository(SchedulerDBContext context, ILogger<Repository> logger)
        {
            _context = context;
            this._logger = logger;
        }
        public async Task<IEnumerable> GetAllLocations()
        {
            try
            {
                var query = "SELECT * FROM DP_Locations";
                using (var connection = _context.CreateConnection())
                {
                    // Use QueryAsync<dynamic> to return dynamic objects
                    var locations = await connection.QueryAsync(query);
                    _logger.LogInformation("Successfully fetched locations from the database.");
                    return locations;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching locations.");
                throw;
            }
            
        }


        // Get Providers Based on Locations
        public async Task<IEnumerable<dynamic>> ProviderByLocations(string dayOfWeek, IEnumerable<int> locationIds)
        {
            
            try
            {
                var query = "USP_DayPilot_Procedure";

                var parameters = new DynamicParameters();
                parameters.Add("@DayOfWeek", dayOfWeek);
                parameters.Add("@LocationIds", string.Join(",", locationIds));

                using (var connection = _context.CreateConnection())
                {
                    var result =  await connection.QueryAsync<dynamic>(query, parameters, commandType: CommandType.StoredProcedure);
                    _logger.LogInformation("Success Providers Based On Location");
                    return result;

                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Providers Based On Location");
                throw;
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
                    var result = await connection.QueryAsync<dynamic>(sql, parameters);

                    _logger.LogInformation("Successfully fetched providers based on location for day: {DayOfWeek}", dayOfWeek);

                    return result;
                }
            }

            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while fetching Providers for Toggel Locations");
                throw;
            }
                
        }

    }
}
