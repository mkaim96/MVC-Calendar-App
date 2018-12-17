using BetterCalendar.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace BetterCalendar.services
{
    public class EventsService
    {
        private IConfiguration _configuration;

        public EventsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<Event> GetAll(string userId)
        {
            using (IDbConnection cnn = GetConnection())
            {
                var sql = @"select * from Events e
                            Where e.UserId = @UserId";

                var p = new { UserId = userId };

                var results = cnn.Query<Event>(sql, p);

                return results.ToList();
            }
        }

        public IEnumerable<Event> GetByMonth(DateTime date, string userId)
        {
            using (IDbConnection cnn = GetConnection())
            {
                var sql = @"select * from Events e
                        Where DATEPART(MONTH, e.Date) = @Month and e.UserId = @UserId";

                var p = new { Month = date.Month, UserId = userId };

                var results = cnn.Query<Event>(sql, p);

                return results.ToList();
            }

        }
        



        #region Helpers 

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("BetterCalendar_Dev"));
        }

        #endregion
    }
}
