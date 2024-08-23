using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPScheduler.DAL.Model
{
    public class SchedulerDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public SchedulerDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("defaultconnection");
        }
            public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        }
    }
