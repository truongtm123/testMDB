using MdbToSqliteLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
namespace TestMDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public List<string> Get()
        {
            //string mdbPath = "/home/user/Database.mdb";
            //string sqlitePath = "/home/user/Database.sqlite";
            ////string mdbPath = "E:\\test4\\Database.mdb";
            ////string sqlitePath = "E:\\test4\\Database.sqlite";
            //MdbConverter.Convert(mdbPath, sqlitePath);

            //var list = new List<string>();
            //string connectionString = "Data Source= " + sqlitePath;
            //using (var connection = new SqliteConnection(connectionString))
            //{
            //    connection.Open();
            //    var selectCmd = new SqliteCommand("SELECT * FROM DEFECTDATA  WHERE SFCS_FLAG = FALSE", connection);
            //    using (var reader = selectCmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            //Console.WriteLine($"BoardNumber: {reader["BoardNumber"]}, ComponentName: {reader["ComponentName"]}");
            //            list.Add(Convert.ToString(reader["DefectCode"]));

            //        }
            //    }


            //    connection.Close();
            //}
            var list = new List<string>();
            list.Add("test1");
            list.Add("test2");
            return list;
        }
    }
}
