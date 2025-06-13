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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public List<string> Get()
        {
            var locaPath = _hostingEnvironment.ContentRootPath + "/" + "Download" + "/" + "MDB_Folder" + "/";
            var sqlLitePah = locaPath + "Database.sqlite";
            sqlLitePah = sqlLitePah.Replace("//", "/");
            sqlLitePah = sqlLitePah.Replace(@"\", "/");



            string output = locaPath.Replace("\\", "/");
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }
            output = locaPath.Replace("//", "/");
            output += "Database.mdb";
            output = output.Replace(@"\", "/");

            //string mdbPath = "/home/user/Database.mdb";
            //string sqlitePath = "/home/user/Database.sqlite";
            ////string mdbPath = "E:\\test4\\Database.mdb";
            ////string sqlitePath = "E:\\test4\\Database.sqlite";
            ///

            MdbConverter.RunCommand("apt update");
            MdbConverter.RunCommand("apt install -y mdbtools sqlite3");
            MdbConverter.Convert(output, sqlLitePah);

            var list = new List<string>();
            string connectionString = "Data Source= " + sqlLitePah;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Delete
                string deleteQuery = "DELETE FROM DEFECTDATA WHERE SFCS_FLAG = TRUE;";
                using (var cmd = new SqliteCommand(deleteQuery, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                }

                string U_UpdateSFCSFlagFinish = @"UPDATE DEFECTDATA SET DefectCode='99' where DefectCode='2'";
                using (var cmd = new SqliteCommand(U_UpdateSFCSFlagFinish, connection))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                }


                var selectCmd = new SqliteCommand("SELECT * FROM DEFECTDATA  WHERE SFCS_FLAG = FALSE;", connection);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine($"BoardNumber: {reader["BoardNumber"]}, ComponentName: {reader["ComponentName"]}");
                        list.Add(Convert.ToString(reader["DefectCode"]) + " and " + Convert.ToString(reader["ComponentName"]));

                    }
                }


                connection.Close();
            }
            return list;
        }
    }
}
